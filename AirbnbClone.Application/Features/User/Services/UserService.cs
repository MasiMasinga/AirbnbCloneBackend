using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Authentication.DTOs;
using AirbnbClone.Application.Features.User.DTOs;
using AirbnbClone.Application.Features.User.Interfaces;
using AirbnbClone.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using UserEntity = AirbnbClone.Domain.Entities.User;
using UserUpdate = AirbnbClone.Domain.Entities.UserUpdate;

namespace AirbnbClone.Application.Features.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<UserEntity> _passwordHasher = new();
    private readonly ILoggerAdapter<UserService> _logger;

    public UserService(
        IUserRepository userRepository,
        ILoggerAdapter<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserDto> GetUserDetails(Guid id, CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching user with id {UserId}", id);

        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user is null)
        {
            _logger.LogWarning("User with id {UserId} was not found", id);
            return null;
        }

        return MapToDto(user);
    }
    
    public async Task<bool> UpdateUserDetails(UpdateUserDto dto, Guid id, CancellationToken ct = default)
    {
        _logger.LogInformation("Updating user with id {UserId}", id);

        var existing = await _userRepository.GetByIdAsync(id, ct);
        if (existing is null)
        {
            _logger.LogWarning("User with id {UserId} was not found for update", id);
            return false;
        }

        var update = new UserUpdate
        {
            Title = dto.Title,
            FirstName = dto.FirstName,
            Surname = dto.Surname,
            EmailAddress = dto.EmailAddress,
            UserRole = dto.UserRole,
            PasswordHash = dto.Password is null
                ? null
                : _passwordHasher.HashPassword(existing, dto.Password),
            Bio = dto.Bio,
            Photo = dto.Photo,
        };

        var updated = await _userRepository.UpdateAsync(update, id, ct);
        _logger.LogInformation("Updated user with id {UserId}", id);
        return updated;
    }
    
    public async Task<bool> DeleteUser(Guid id, CancellationToken ct = default)
    {
        _logger.LogInformation("Deleting user with id {ListingId}", id);

        var existing = await _userRepository.GetByIdAsync(id, ct);
        if (existing is null)
        {
            _logger.LogWarning("User with id {UserId} was not found for delete", id);
            return false;
        }

        await _userRepository.DeleteAsync(id, ct);
        _logger.LogInformation("Deleted user with id {UserId}", id);
        return true;
    }
    
    private static UserDto MapToDto(UserEntity user) => new()
    {
        Id = user.Id,
        Title = user.Title,
        FirstName = user.FirstName,
        Surname = user.Surname,
        EmailAddress = user.EmailAddress,
        Password = user.Password,
        Bio = user.Bio,
        Photo = user.Photo,
    };
}
