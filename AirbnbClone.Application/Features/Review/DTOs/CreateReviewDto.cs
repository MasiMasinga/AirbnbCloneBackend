namespace AirbnbClone.Application.Features.Review.DTOs;

public class CreateReviewDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int ListingId { get; set; }
    public int BookingId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public int Cleanliness { get; set; }
    public int Accuracy { get; set; }
    public int CheckIn { get; set; }
    public int Communication { get; set; }
    public int Location { get; set; }
    public int Value { get; set; }
    public int Overall { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly UpdatedAt { get; set; }
}