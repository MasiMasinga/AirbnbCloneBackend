using AirbnbClone.Application.Features.Listings.DTOs;

namespace AirbnbClone.Application.Features.Listings.Interfaces;

public interface IListingService
{
    Task<ListingDto?> GetListingById(int id, CancellationToken ct = default);
    Task<IEnumerable<ListingDto>> GetAllListings(CancellationToken ct = default);
    Task<ListingDto> CreateListing(CreateListingDto dto, CancellationToken ct = default);
    Task<ListingDto?> UpdateListing(UpdateListingDto dto, CancellationToken ct = default);
    Task<bool> DeleteListing(int id, CancellationToken ct = default);
}
