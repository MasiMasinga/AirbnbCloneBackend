using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Domain.Interfaces;

public interface IListingRepository
{
    Task<IEnumerable<Listings>> GetAllAsync(CancellationToken ct = default);
    Task<Listings?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<int> CreateAsync(Listings listing, CancellationToken ct = default);
    Task UpdateAsync(Listings listing, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
