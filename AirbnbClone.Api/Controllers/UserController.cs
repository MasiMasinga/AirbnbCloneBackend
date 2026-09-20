using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.User.DTOs;
using AirbnbClone.Application.Features.User.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<UpdateUserDto> _updateValidator;
    private readonly ILoggerAdapter<UserController> _logger;

    public UserController(
        IUserService userService,
        IValidator<UpdateUserDto> updateValidator, 
        ILoggerAdapter<UserController> logger)
    {
        _userService = userService;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var userDetails = await _userService.GetUserDetails(id, ct);
        
        if (userDetails is null)
        {
            _logger.LogWarning("User {UserId} not found", id);
            return NotFound();
        }

        return Ok(userDetails);
    }

    [HttpPatch("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> Update(UpdateUserDto dto, Guid id, CancellationToken ct)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto, ct);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Update user validation failed for user {UserId}", id);
            return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
        }

        var updatedUser = await _userService.UpdateUserDetails(dto, id, ct);
        if (!updatedUser)
        {
            _logger.LogWarning("User {UserId} not found for update", id);
            return NotFound();
        }

        _logger.LogInformation("Updated User {UserId}", id);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("DELETE /api/user/{ListingId}", id);

        var deleted = await _userService.DeleteUser(id, ct);
        if (!deleted)
        {
            _logger.LogWarning("User {UserId} not found for delete", id);
            return NotFound();
        }

        _logger.LogInformation("Deleted User {UserId}", id);
        return NoContent();
    }
}
