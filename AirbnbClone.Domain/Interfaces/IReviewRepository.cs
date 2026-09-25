using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Domain.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync(CancellationToken ct = default);
    Task<Review?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<int> CreateAsync(Review review, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}