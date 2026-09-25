using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Review.DTOs;
using AirbnbClone.Application.Features.Review.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbClone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IValidator<CreateReviewDto> _createValidator;
    private readonly ILoggerAdapter<ReviewController> _logger;

    public ReviewController(
            IReviewService reviewService,
            IValidator<CreateReviewDto> createValidator,
            ILoggerAdapter<ReviewController> logger)
    {
        _reviewService = reviewService;
        _createValidator = createValidator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("GET /api/review");

        var bookings = await _reviewService.GetAllReviews(ct);
        return Ok(bookings);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        _logger.LogInformation("GET /api/review/{BookingId}", id);

        var booking = await _reviewService.GetReviewById(id, ct);
        if (booking is null)
        {
            _logger.LogWarning("Review {ReviewId} not found", id);
            return NotFound();
        }

        return Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewDto dto, CancellationToken ct)
    {
        _logger.LogInformation("POST /api/review for user {UserId}", dto.UserId);

        var validationResult = await _createValidator.ValidateAsync(dto, ct);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for creating review: {Errors}", validationResult.Errors);
            return BadRequest(validationResult.Errors);
        }

        var booking = await _reviewService.CreateReview(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        _logger.LogInformation("DELETE /api/review/{ReviewId}", id);

        var review = await _reviewService.DeleteReview(id, ct);
        if (!review)
        {
            _logger.LogWarning("Review {ReviewId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}