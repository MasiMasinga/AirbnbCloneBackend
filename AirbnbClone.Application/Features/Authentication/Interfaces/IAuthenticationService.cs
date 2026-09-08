using AirbnbClone.Application.Features.Authentication.DTOs;

namespace AirbnbClone.Application.Features.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<UserDto> SignUp(CreateUserDto dto, CancellationToken ct = default);
    Task<UserDto> Login(LoginUserDto dto, CancellationToken ct = default);
}