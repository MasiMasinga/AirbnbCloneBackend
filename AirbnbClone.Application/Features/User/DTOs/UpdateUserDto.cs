namespace AirbnbClone.Application.Features.User.DTOs;

public sealed class UpdateUserDto
{
    public string? Title { get; init; }
    public string? UserRole { get; init; }
    public string? FirstName { get; init; }
    public string? Surname { get; init; }
    public string? EmailAddress { get; init; }
    public string? Password { get; init; }
    public string? Bio { get; init; }
    public string? Photo { get; init; }
}
