namespace AirbnbClone.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string UserRole { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; } = string.Empty;
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public DateOnly CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}