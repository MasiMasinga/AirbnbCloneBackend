using AirbnbClone.Application.Features.Listings.Validators;

namespace AirbnbClone.Application.Tests.Features.Listings.Validators;

public class UpdateListingValidatorTests
{
    private readonly UpdateListingValidator _sut = new();

    [Fact]
    public void ValidDto_Passes()
    {
        var result = _sut.Validate(ListingTestData.ValidUpdateDto());

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Id_WhenNotPositive_Fails(int id)
    {
        var dto = ListingTestData.ValidUpdateDto(d => d.Id = id);

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Id));
    }

    [Fact]
    public void Title_WhenEmpty_Fails()
    {
        var dto = ListingTestData.ValidUpdateDto(d => d.Title = "");

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Title));
    }

    [Fact]
    public void Pricing_WhenNegative_Fails()
    {
        var dto = ListingTestData.ValidUpdateDto(d => d.Pricing = -5);

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Pricing));
    }

    [Fact]
    public void EndDate_WhenNotAfterStartDate_Fails()
    {
        var dto = ListingTestData.ValidUpdateDto(d =>
        {
            d.StartDate = new DateOnly(2026, 9, 10);
            d.EndDate = new DateOnly(2026, 9, 1);
        });

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.EndDate));
    }

    [Fact]
    public void Photo_WhenEmpty_Fails()
    {
        var dto = ListingTestData.ValidUpdateDto(d => d.Photo = "");

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Photo));
    }
}
