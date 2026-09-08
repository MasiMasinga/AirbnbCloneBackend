namespace AirbnbClone.Application.Features.Authentication.DTOs;

public class CreateUserDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string UserRole { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string Bio { get; set; }
    public string Photo { get; set; } = string.Empty;
    public DateOnly CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}