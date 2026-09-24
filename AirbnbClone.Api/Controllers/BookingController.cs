using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Booking.DTOs;
using AirbnbClone.Application.Features.Booking.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IValidator<CreateBookingDto> _createValidator;
    private readonly IValidator<UpdateBookingDto> _updateValidator;
    private readonly ILoggerAdapter<BookingController> _logger;

    public BookingController(
        IBookingService bookingService,
        IValidator<CreateBookingDto> createValidator,
        IValidator<UpdateBookingDto> updateValidator,
        ILoggerAdapter<BookingController> logger)
    {
        _bookingService = bookingService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("GET /api/booking");

        var bookings = await _bookingService.GetAllBookings(ct);
        return Ok(bookings);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        _logger.LogInformation("GET /api/booking/{BookingId}", id);

        var booking = await _bookingService.GetBookingById(id, ct);
        if (booking is null)
        {
            _logger.LogWarning("Booking {BookingId} not found", id);
            return NotFound();
        }

        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingDto dto, CancellationToken ct)
    {
        _logger.LogInformation("POST /api/booking for user {UserId}", dto.UserId);

        var validationResult = await _createValidator.ValidateAsync(dto, ct);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for creating booking: {Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        var booking = await _bookingService.CreateBooking(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateBookingDto dto, CancellationToken ct)
    {
        _logger.LogInformation("PUT /api/booking/{BookingId} for user {UserId}", id, dto.UserId);

        var validationResult = await _updateValidator.ValidateAsync(dto, ct);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for updating booking: {Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        var updatedBooking = await _bookingService.UpdateBooking(id, dto, ct);
        if (updatedBooking is null)
        {
            _logger.LogWarning("Booking {BookingId} not found for update", id);
            return NotFound();
        }

        return Ok(updatedBooking);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        _logger.LogInformation("DELETE /api/booking/{BookingId}", id);

        var success = await _bookingService.DeleteBooking(id, ct);
        if (!success)
        {
            _logger.LogWarning("Booking {BookingId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}