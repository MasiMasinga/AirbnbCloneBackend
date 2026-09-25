using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Booking.DTOs;
using AirbnbClone.Application.Features.Booking.Interfaces;
using AirbnbClone.Domain.Interfaces;
using BookingEntity = AirbnbClone.Domain.Entities.Booking;

namespace AirbnbClone.Application.Features.Booking.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ILoggerAdapter<BookingService> _logger;

    public BookingService(
        IBookingRepository bookingRepository,
        ILoggerAdapter<BookingService> logger)
    {
        _bookingRepository = bookingRepository;
        _logger = logger;
    }

    public async Task<BookingDto?> GetBookingById(int id, CancellationToken ct = default)
    {
        var booking = await _bookingRepository.GetByIdAsync(id, ct);
        if (booking == null)
        {
            _logger.LogWarning($"Booking with ID {id} not found.");
            return null;
        }

        return MapToDto(booking);
    }

    public async Task<IEnumerable<BookingDto>> GetAllBookings(CancellationToken ct = default)
    {
        var bookings = await _bookingRepository.GetAllAsync(ct);
        return bookings.Select(MapToDto);
    }

    public async Task<BookingDto> CreateBooking(CreateBookingDto dto, CancellationToken ct = default)
    {
        var booking = new BookingEntity
        {
            ListingId = dto.ListingId,
            UserId = dto.UserId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Amount = dto.Amount,
            NumberOfGuests = dto.NumberOfGuests,
        };

        var bookingId = await _bookingRepository.CreateAsync(booking, ct);
        booking.Id = bookingId;

        return MapToDto(booking);
    }

    public async Task<BookingDto?> UpdateBooking(int id, UpdateBookingDto dto, CancellationToken ct = default)
    {
        var existingBooking = await _bookingRepository.GetByIdAsync(dto.Id, ct);
        if (existingBooking == null)
        {
            _logger.LogWarning($"Booking with ID {dto.Id} not found.");
            return null;
        }

        existingBooking.ListingId = dto.ListingId;
        existingBooking.UserId = dto.UserId;
        existingBooking.StartDate = dto.StartDate;
        existingBooking.EndDate = dto.EndDate;
        existingBooking.Amount = dto.Amount;
        existingBooking.NumberOfGuests = dto.NumberOfGuests;

        await _bookingRepository.UpdateAsync(existingBooking, ct);

        return MapToDto(existingBooking);
    }

    public async Task<bool> DeleteBooking(int id, CancellationToken ct = default)
    {
        var existingBooking = await _bookingRepository.GetByIdAsync(id, ct);
        if (existingBooking == null)
        {
            _logger.LogWarning($"Booking with ID {id} not found.");
            return false;
        }

        await _bookingRepository.DeleteAsync(id, ct);
        return true;
    }

    private static BookingDto MapToDto(BookingEntity booking) => new()
    {
        Id = booking.Id,
        ListingId = booking.ListingId,
        UserId = booking.UserId,
        StartDate = booking.StartDate,
        EndDate = booking.EndDate,
        Amount = booking.Amount,
        NumberOfGuests = booking.NumberOfGuests,
    };
}
