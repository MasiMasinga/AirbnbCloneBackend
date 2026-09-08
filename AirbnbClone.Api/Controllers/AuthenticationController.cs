using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Authentication.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AirbnbClone.Application.Features.Authentication.Interfaces;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IValidator<CreateUserDto> _createValidator;
    private readonly IValidator<LoginUserDto> _loginValidator;
    private readonly ILoggerAdapter<AuthenticationController> _logger;

    public AuthenticationController(
        IAuthenticationService authenticationService,
        IValidator<CreateUserDto> createValidator, 
        IValidator<LoginUserDto> loginValidator,
        ILoggerAdapter<AuthenticationController> logger)
    {
        _authenticationService = authenticationService;
        _loginValidator = loginValidator;
        _createValidator = createValidator;
        _logger = logger;
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(CreateUserDto dto, CancellationToken ct)
    {
        if (dto == null)
        {
            return BadRequest(new { message = "Request body is required." });
        }
        
        try
        {
            var validationResult = await _createValidator.ValidateAsync(dto, ct);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Create user validation failed for user {UserId}", dto.Id);
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            var user = await _authenticationService.SignUp(dto, ct);
            _logger.LogInformation("Created user {ListingId}", user.Id);
            
            return StatusCode(StatusCodes.Status201Created, user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserDto dto, CancellationToken ct)
    {
        if (dto == null)
        {
            return BadRequest(new { message = "Request body is required." });
        }
        
        try
        {
            var validationResult = await _loginValidator.ValidateAsync(dto, ct);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Login user validation failed for {EmailAddress}", dto.EmailAddress);
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            var user = await _authenticationService.Login(dto, ct);
            _logger.LogInformation("Logged in user {UserId}", user.Id);
            
            return StatusCode(StatusCodes.Status200OK, user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}