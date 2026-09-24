using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Domain.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAllAsync(CancellationToken ct = default);
    Task<Booking?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<int> CreateAsync(Booking booking, CancellationToken ct = default);
    Task UpdateAsync(Booking booking, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}