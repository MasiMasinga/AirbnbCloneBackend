using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Domain.Interfaces;

public interface IAuthenticationRepository
{
    Task<Guid> SignUp(User user, CancellationToken ct = default);
    Task Login(User user, CancellationToken ct = default);
    Task<User?> CheckExistingUser(string emailAdress, CancellationToken ct = default);
}