using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Listings.DTOs;
using AirbnbClone.Application.Features.Listings.Interfaces;
using AirbnbClone.Domain.Interfaces;
using ListingEntity = AirbnbClone.Domain.Entities.Listings;

namespace AirbnbClone.Application.Features.Listings.Services;

public class ListingService : IListingService
{
    private readonly IListingRepository _listingRepository;
    private readonly ILoggerAdapter<ListingService> _logger;

    public ListingService(
        IListingRepository listingRepository,
        ILoggerAdapter<ListingService> logger)
    {
        _listingRepository = listingRepository;
        _logger = logger;
    }

    public async Task<ListingDto?> GetListingById(int id, CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching listing with id {ListingId}", id);

        var listing = await _listingRepository.GetByIdAsync(id, ct);
        if (listing is null)
        {
            _logger.LogWarning("Listing with id {ListingId} was not found", id);
            return null;
        }

        return MapToDto(listing);
    }

    public async Task<IEnumerable<ListingDto>> GetAllListings(CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching all listings");

        var listings = await _listingRepository.GetAllAsync(ct);
        return listings.Select(MapToDto);
    }

    public async Task<ListingDto> CreateListing(CreateListingDto dto, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating listing for user {UserId} with title {Title}", dto.UserId, dto.Title);

        var entity = new ListingEntity
        {
            Title = dto.Title,
            Description = dto.Description,
            Amenities = dto.Amenities,
            HouseRules = dto.HouseRules,
            Pricing = dto.Pricing,
            Availability = dto.Availability,
            BedCount = dto.BedCount,
            BathCount = dto.BathCount,
            PropertyType = dto.PropertyType,
            Address = dto.Address,
            Location = dto.Location,
            Country = dto.Country,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Photo = dto.Photo,
            UserId = dto.UserId
        };

        var id = await _listingRepository.CreateAsync(entity, ct);
        entity.Id = id;

        _logger.LogInformation("Created listing with id {ListingId}", id);
        return MapToDto(entity);
    }

    public async Task<ListingDto?> UpdateListing(UpdateListingDto dto, CancellationToken ct = default)
    {
        _logger.LogInformation("Updating listing with id {ListingId}", dto.Id);

        var existing = await _listingRepository.GetByIdAsync(dto.Id, ct);
        if (existing is null)
        {
            _logger.LogWarning("Listing with id {ListingId} was not found for update", dto.Id);
            return null;
        }

        var entity = new ListingEntity
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Amenities = dto.Amenities,
            HouseRules = dto.HouseRules,
            Pricing = dto.Pricing,
            Availability = dto.Availability,
            BedCount = dto.BedCount,
            BathCount = dto.BathCount,
            PropertyType = dto.PropertyType,
            Address = dto.Address,
            Location = dto.Location,
            Country = dto.Country,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Photo = dto.Photo
        };

        await _listingRepository.UpdateAsync(entity, ct);
        _logger.LogInformation("Updated listing with id {ListingId}", dto.Id);
        return MapToDto(entity);
    }

    public async Task<bool> DeleteListing(int id, CancellationToken ct = default)
    {
        _logger.LogInformation("Deleting listing with id {ListingId}", id);

        var existing = await _listingRepository.GetByIdAsync(id, ct);
        if (existing is null)
        {
            _logger.LogWarning("Listing with id {ListingId} was not found for delete", id);
            return false;
        }

        await _listingRepository.DeleteAsync(id, ct);
        _logger.LogInformation("Deleted listing with id {ListingId}", id);
        return true;
    }

    private static ListingDto MapToDto(ListingEntity listing) => new()
    {
        Id = listing.Id,
        Title = listing.Title,
        Description = listing.Description,
        Amenities = listing.Amenities,
        HouseRules = listing.HouseRules,
        Pricing = listing.Pricing,
        Availability = listing.Availability,
        BedCount = listing.BedCount,
        BathCount = listing.BathCount,
        PropertyType = listing.PropertyType,
        Address = listing.Address,
        Location = listing.Location,
        Country = listing.Country,
        StartDate = listing.StartDate,
        EndDate = listing.EndDate,
        Photo = listing.Photo,
        UserId = listing.UserId,
        ReviewId = listing.ReviewId
    };
}
