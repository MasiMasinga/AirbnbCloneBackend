using AirbnbClone.Api.Controllers;
using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Listings.DTOs;
using AirbnbClone.Application.Features.Listings.Interfaces;
using AirbnbClone.Application.Features.Listings.Validators;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AirbnbClone.Api.Tests.Controllers;

public class ListingsControllerTests
{
    private readonly Mock<IListingService> _listingService = new();
    private readonly Mock<ILoggerAdapter<ListingsController>> _logger = new();
    private readonly ListingsController _sut;

    public ListingsControllerTests()
    {
        _sut = new ListingsController(
            _listingService.Object,
            new CreateListingValidator(),
            new UpdateListingValidator(),
            _logger.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithListings()
    {
        var listings = new[]
        {
            new ListingDto { Id = 1, Title = "Beach House" },
            new ListingDto { Id = 2, Title = "Cabin" }
        };
        _listingService
            .Setup(s => s.GetAllListings(It.IsAny<CancellationToken>()))
            .ReturnsAsync(listings);

        var result = await _sut.GetAll(CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(listings, ok.Value);
    }

    [Fact]
    public async Task GetById_WhenFound_ReturnsOk()
    {
        var listing = new ListingDto { Id = 1, Title = "Beach House" };
        _listingService
            .Setup(s => s.GetListingById(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(listing);

        var result = await _sut.GetById(1, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(listing, ok.Value);
    }

    [Fact]
    public async Task GetById_WhenNotFound_ReturnsNotFound()
    {
        _listingService
            .Setup(s => s.GetListingById(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ListingDto?)null);

        var result = await _sut.GetById(99, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_WhenValid_ReturnsCreatedAtAction()
    {
        var dto = ValidCreateDto();
        var created = new ListingDto { Id = 10, Title = dto.Title, UserId = dto.UserId };
        _listingService
            .Setup(s => s.CreateListing(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(created);

        var result = await _sut.Create(dto, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ListingsController.GetById), createdResult.ActionName);
        Assert.Equal(10, createdResult.RouteValues?["id"]);
        Assert.Same(created, createdResult.Value);
    }

    [Fact]
    public async Task Create_WhenInvalid_ReturnsValidationProblem()
    {
        var dto = ValidCreateDto();
        dto.Title = "";

        var result = await _sut.Create(dto, CancellationToken.None);

        Assert.IsAssignableFrom<ObjectResult>(result);
        _listingService.Verify(
            s => s.CreateListing(It.IsAny<CreateListingDto>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Update_WhenRouteAndBodyIdMismatch_ReturnsBadRequest()
    {
        var dto = ValidUpdateDto();
        dto.Id = 2;

        var result = await _sut.Update(1, dto, CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Route id and body id must match.", badRequest.Value);
        _listingService.Verify(
            s => s.UpdateListing(It.IsAny<UpdateListingDto>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Update_WhenInvalid_ReturnsValidationProblem()
    {
        var dto = ValidUpdateDto();
        dto.Title = "";

        var result = await _sut.Update(1, dto, CancellationToken.None);

        Assert.IsAssignableFrom<ObjectResult>(result);
        _listingService.Verify(
            s => s.UpdateListing(It.IsAny<UpdateListingDto>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Update_WhenFound_ReturnsOk()
    {
        var dto = ValidUpdateDto();
        var updated = new ListingDto { Id = 1, Title = dto.Title };
        _listingService
            .Setup(s => s.UpdateListing(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updated);

        var result = await _sut.Update(1, dto, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(updated, ok.Value);
    }

    [Fact]
    public async Task Update_WhenNotFound_ReturnsNotFound()
    {
        var dto = ValidUpdateDto();
        _listingService
            .Setup(s => s.UpdateListing(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ListingDto?)null);

        var result = await _sut.Update(1, dto, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_WhenFound_ReturnsNoContent()
    {
        _listingService
            .Setup(s => s.DeleteListing(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _sut.Delete(1, CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_WhenNotFound_ReturnsNotFound()
    {
        _listingService
            .Setup(s => s.DeleteListing(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _sut.Delete(99, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    private static CreateListingDto ValidCreateDto() => new()
    {
        Title = "Cozy Beach House",
        Description = "A lovely beach house with ocean views.",
        Amenities = "Wifi, Pool, Parking",
        HouseRules = "No smoking, No pets",
        Pricing = 150.00m,
        Availability = true,
        BedCount = 2,
        BathCount = 1,
        PropertyType = "House",
        Address = "123 Ocean Drive",
        Location = "Cape Town",
        Country = "South Africa",
        StartDate = new DateOnly(2026, 8, 1),
        EndDate = new DateOnly(2026, 8, 15),
        Photo = "https://example.com/photo.jpg",
        UserId = Guid.Parse("11111111-1111-1111-1111-111111111111")
    };

    private static UpdateListingDto ValidUpdateDto() => new()
    {
        Id = 1,
        Title = "Updated Beach House",
        Description = "An updated beach house with ocean views.",
        Amenities = "Wifi, Pool, Parking, Gym",
        HouseRules = "No smoking",
        Pricing = 175.00m,
        Availability = true,
        BedCount = 3,
        BathCount = 2,
        PropertyType = "House",
        Address = "123 Ocean Drive",
        Location = "Cape Town",
        Country = "South Africa",
        StartDate = new DateOnly(2026, 9, 1),
        EndDate = new DateOnly(2026, 9, 20),
        Photo = "https://example.com/updated.jpg"
    };
}
