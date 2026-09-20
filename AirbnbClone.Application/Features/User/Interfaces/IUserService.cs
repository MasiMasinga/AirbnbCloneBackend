using AirbnbClone.Application.Features.Authentication.DTOs;
using AirbnbClone.Application.Features.User.DTOs;

namespace AirbnbClone.Application.Features.User.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserDetails(Guid id, CancellationToken ct = default);
    Task<bool> UpdateUserDetails(UpdateUserDto dto, Guid id, CancellationToken ct = default);
    Task<bool> DeleteUser(Guid id, CancellationToken ct = default);
}
