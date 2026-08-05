using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Listings.Services;
using AirbnbClone.Domain.Interfaces;
using Moq;
using ListingEntity = AirbnbClone.Domain.Entities.Listings;

namespace AirbnbClone.Application.Tests.Features.Listings.Services;

public class ListingServiceTests
{
    private readonly Mock<IListingRepository> _repository = new();
    private readonly Mock<ILoggerAdapter<ListingService>> _logger = new();
    private readonly ListingService _sut;

    public ListingServiceTests()
    {
        _sut = new ListingService(_repository.Object, _logger.Object);
    }

    [Fact]
    public async Task GetListingById_WhenExists_ReturnsMappedDto()
    {
        var entity = ListingTestData.Entity();
        _repository
            .Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        var result = await _sut.GetListingById(1);

        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal(entity.Title, result.Title);
        Assert.Equal(entity.Description, result.Description);
        Assert.Equal(entity.Amenities, result.Amenities);
        Assert.Equal(entity.HouseRules, result.HouseRules);
        Assert.Equal(entity.Pricing, result.Pricing);
        Assert.Equal(entity.Availability, result.Availability);
        Assert.Equal(entity.BedCount, result.BedCount);
        Assert.Equal(entity.BathCount, result.BathCount);
        Assert.Equal(entity.PropertyType, result.PropertyType);
        Assert.Equal(entity.Address, result.Address);
        Assert.Equal(entity.Location, result.Location);
        Assert.Equal(entity.Country, result.Country);
        Assert.Equal(entity.StartDate, result.StartDate);
        Assert.Equal(entity.EndDate, result.EndDate);
        Assert.Equal(entity.Photo, result.Photo);
        Assert.Equal(entity.UserId, result.UserId);
        Assert.Equal(entity.ReviewId, result.ReviewId);
    }

    [Fact]
    public async Task GetListingById_WhenNotFound_ReturnsNull()
    {
        _repository
            .Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ListingEntity?)null);

        var result = await _sut.GetListingById(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllListings_ReturnsMappedDtos()
    {
        var entities = new[]
        {
            ListingTestData.Entity(e => e.Id = 1),
            ListingTestData.Entity(e =>
            {
                e.Id = 2;
                e.Title = "Mountain Cabin";
            })
        };

        _repository
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(entities);

        var result = (await _sut.GetAllListings()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Cozy Beach House", result[0].Title);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Mountain Cabin", result[1].Title);
    }

    [Fact]
    public async Task GetAllListings_WhenEmpty_ReturnsEmptyCollection()
    {
        _repository
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await _sut.GetAllListings();

        Assert.Empty(result);
    }

    [Fact]
    public async Task CreateListing_PersistsEntityAndReturnsDtoWithId()
    {
        var dto = ListingTestData.ValidCreateDto();
        ListingEntity? captured = null;

        _repository
            .Setup(r => r.CreateAsync(It.IsAny<ListingEntity>(), It.IsAny<CancellationToken>()))
            .Callback<ListingEntity, CancellationToken>((entity, _) => captured = entity)
            .ReturnsAsync(42);

        var result = await _sut.CreateListing(dto);

        Assert.Equal(42, result.Id);
        Assert.Equal(dto.Title, result.Title);
        Assert.Equal(dto.UserId, result.UserId);
        Assert.Equal(dto.Pricing, result.Pricing);
        Assert.NotNull(captured);
        Assert.Equal(dto.Title, captured.Title);
        Assert.Equal(dto.Description, captured.Description);
        Assert.Equal(dto.BedCount, captured.BedCount);
        Assert.Equal(dto.UserId, captured.UserId);
        _repository.Verify(
            r => r.CreateAsync(It.IsAny<ListingEntity>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateListing_WhenExists_UpdatesAndReturnsDto()
    {
        var dto = ListingTestData.ValidUpdateDto();
        _repository
            .Setup(r => r.GetByIdAsync(dto.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ListingTestData.Entity());
        _repository
            .Setup(r => r.UpdateAsync(It.IsAny<ListingEntity>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _sut.UpdateListing(dto);

        Assert.NotNull(result);
        Assert.Equal(dto.Id, result.Id);
        Assert.Equal(dto.Title, result.Title);
        Assert.Equal(dto.Pricing, result.Pricing);
        Assert.Equal(dto.BedCount, result.BedCount);
        _repository.Verify(
            r => r.UpdateAsync(
                It.Is<ListingEntity>(e => e.Id == dto.Id && e.Title == dto.Title),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateListing_WhenNotFound_ReturnsNull()
    {
        var dto = ListingTestData.ValidUpdateDto(d => d.Id = 99);
        _repository
            .Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ListingEntity?)null);

        var result = await _sut.UpdateListing(dto);

        Assert.Null(result);
        _repository.Verify(
            r => r.UpdateAsync(It.IsAny<ListingEntity>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task DeleteListing_WhenExists_DeletesAndReturnsTrue()
    {
        _repository
            .Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ListingTestData.Entity());
        _repository
            .Setup(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _sut.DeleteListing(1);

        Assert.True(result);
        _repository.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteListing_WhenNotFound_ReturnsFalse()
    {
        _repository
            .Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ListingEntity?)null);

        var result = await _sut.DeleteListing(99);

        Assert.False(result);
        _repository.Verify(
            r => r.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
