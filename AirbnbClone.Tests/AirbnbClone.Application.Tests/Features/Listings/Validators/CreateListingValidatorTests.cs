using AirbnbClone.Application.Features.Listings.Validators;

namespace AirbnbClone.Application.Tests.Features.Listings.Validators;

public class CreateListingValidatorTests
{
    private readonly CreateListingValidator _sut = new();

    [Fact]
    public void ValidDto_Passes()
    {
        var result = _sut.Validate(ListingTestData.ValidCreateDto());

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Title_WhenEmpty_Fails(string title)
    {
        var dto = ListingTestData.ValidCreateDto(d => d.Title = title);

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Title));
    }

    [Fact]
    public void Title_WhenTooLong_Fails()
    {
        var dto = ListingTestData.ValidCreateDto(d => d.Title = new string('a', 101));

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Title));
    }

    [Fact]
    public void Description_WhenTooLong_Fails()
    {
        var dto = ListingTestData.ValidCreateDto(d => d.Description = new string('a', 141));

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Description));
    }

    [Fact]
    public void Pricing_WhenNegative_Fails()
    {
        var dto = ListingTestData.ValidCreateDto(d => d.Pricing = -1);

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Pricing));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void BedCount_WhenNotPositive_Fails(int bedCount)
    {
        var dto = ListingTestData.ValidCreateDto(d => d.BedCount = bedCount);

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.BedCount));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void BathCount_WhenNotPositive_Fails(int bathCount)
    {
        var dto = ListingTestData.ValidCreateDto(d => d.BathCount = bathCount);

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.BathCount));
    }

    [Fact]
    public void UserId_WhenEmpty_Fails()
    {
        var dto = ListingTestData.ValidCreateDto(d => d.UserId = Guid.Empty);

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.UserId));
    }

    [Fact]
    public void Photo_WhenEmpty_Fails()
    {
        var dto = ListingTestData.ValidCreateDto(d => d.Photo = "");

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.Photo));
    }

    [Fact]
    public void EndDate_WhenNotAfterStartDate_Fails()
    {
        var dto = ListingTestData.ValidCreateDto(d =>
        {
            d.StartDate = new DateOnly(2026, 8, 15);
            d.EndDate = new DateOnly(2026, 8, 15);
        });

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.EndDate));
    }

    [Fact]
    public void PropertyType_WhenTooLong_Fails()
    {
        var dto = ListingTestData.ValidCreateDto(d => d.PropertyType = new string('a', 21));

        var result = _sut.Validate(dto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(dto.PropertyType));
    }
}
