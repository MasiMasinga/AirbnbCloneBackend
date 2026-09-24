using AirbnbClone.Application.Features.Booking.DTOs;

namespace AirbnbClone.Application.Features.Booking.Interfaces;

public interface IBookingService
{
    Task<BookingDto?> GetBookingById(int id, CancellationToken ct = default);
    Task<IEnumerable<BookingDto>> GetAllBookings(CancellationToken ct = default);
    Task<BookingDto> CreateBooking(CreateBookingDto dto, CancellationToken ct = default);
    Task<BookingDto?> UpdateBooking(int id, UpdateBookingDto dto, CancellationToken ct = default);
    Task<bool> DeleteBooking(int id, CancellationToken ct = default);
}
