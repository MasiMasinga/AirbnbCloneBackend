namespace AirbnbClone.Application.Features.Authentication.DTOs;

public class CreateUserDto
{
    public Guid Id { get; set; }
    public string UserRole { get; set; }
    public string FirstName { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}