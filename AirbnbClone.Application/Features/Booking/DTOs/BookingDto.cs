namespace AirbnbClone.Application.Features.Booking.DTOs;

public class BookingDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int ListingId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Amount { get; set; }
    public int NumberOfGuests { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly UpdatedAt { get; set; }
}