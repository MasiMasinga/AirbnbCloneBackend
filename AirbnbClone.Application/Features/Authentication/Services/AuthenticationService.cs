using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Common.Helpers;
using AirbnbClone.Application.Features.Authentication.DTOs;
using AirbnbClone.Application.Features.Authentication.Interfaces;
using AirbnbClone.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UserEntity = AirbnbClone.Domain.Entities.User;

namespace AirbnbClone.Application.Features.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly PasswordHasher<UserEntity> _passwordHasher = new();
    private readonly IConfiguration _configuration;
    private readonly ILoggerAdapter<AuthenticationService> _logger;

    public AuthenticationService(
        IAuthenticationRepository authenticationRepository,
        IConfiguration configuration,
        ILoggerAdapter<AuthenticationService> logger)
    {
        _authenticationRepository = authenticationRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<UserDto> SignUp(CreateUserDto dto, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Creating user {UserId} with title {Title}", dto.Id, dto.FirstName);

            var newUser = new UserEntity
            {
                UserRole = dto.UserRole,
                FirstName = dto.FirstName,
                EmailAddress = dto.EmailAddress.Trim().ToLowerInvariant(),
                Password = dto.Password,
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.Id = await _authenticationRepository.SignUp(newUser, ct);

            _logger.LogInformation("Created user with id {UserId}", newUser.Id);

            return new UserDto
            {
                Id = newUser.Id,
                UserRole = newUser.UserRole,
                FirstName = newUser.FirstName,
                EmailAddress = newUser.EmailAddress,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError( "Failed to create user", ex.Message);
            throw;
        }
    }

    public async Task<UserDto> Login(LoginUserDto dto, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Login user {EmailAddress}", dto.EmailAddress);

            var identifier = dto.EmailAddress.Trim();
            var normalized = identifier.ToLowerInvariant();

            var existingUser = await _authenticationRepository.CheckExistingUser(normalized, ct);

            if (existingUser == null || string.IsNullOrEmpty(existingUser.Password))
            {
                throw new Exception("Invalid email/username or password.");
            }

            var verifyResult = _passwordHasher.VerifyHashedPassword(
                existingUser, existingUser.Password, dto.Password);
            if (verifyResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid email/username or password.");
            }

            var accessToken = JwtTokenHelper.GenerateJwtAccessToken(_configuration, existingUser);

            return new UserDto
            {
                Id = existingUser.Id,
                FirstName = existingUser.FirstName,
                Surname = existingUser.Surname,
                EmailAddress = existingUser.EmailAddress,
                UserRole = existingUser.UserRole,
                Access = accessToken
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to login user", ex.Message);
            throw;
        }
    }
}