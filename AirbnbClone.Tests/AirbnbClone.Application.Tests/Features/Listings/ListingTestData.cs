using AirbnbClone.Application.Features.Listings.DTOs;
using ListingEntity = AirbnbClone.Domain.Entities.Listings;

namespace AirbnbClone.Application.Tests.Features.Listings;

internal static class ListingTestData
{
    public static Guid DefaultUserId { get; } = Guid.Parse("11111111-1111-1111-1111-111111111111");

    public static CreateListingDto ValidCreateDto(Action<CreateListingDto>? configure = null)
    {
        var dto = new CreateListingDto
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
            UserId = DefaultUserId
        };

        configure?.Invoke(dto);
        return dto;
    }

    public static UpdateListingDto ValidUpdateDto(Action<UpdateListingDto>? configure = null)
    {
        var dto = new UpdateListingDto
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

        configure?.Invoke(dto);
        return dto;
    }

    public static ListingEntity Entity(Action<ListingEntity>? configure = null)
    {
        var entity = new ListingEntity
        {
            Id = 1,
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
            UserId = DefaultUserId,
            ReviewId = "review-1"
        };

        configure?.Invoke(entity);
        return entity;
    }
}
