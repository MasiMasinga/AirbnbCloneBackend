using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> UpdateAsync(UserUpdate update, Guid id, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
