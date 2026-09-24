namespace AirbnbClone.Application.Features.Booking.DTOs;

public class UpdateBookingDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int ListingId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Amount { get; set; }
    public int NumberOfGuests { get; set; }
}