using AirbnbClone.Application.Common.Abstractions;
using AirbnbClone.Application.Features.Review.DTOs;
using AirbnbClone.Application.Features.Review.Interfaces;
using AirbnbClone.Domain.Interfaces;
using ReviewEntity = AirbnbClone.Domain.Entities.Review;

namespace AirbnbClone.Application.Features.Review.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ILoggerAdapter<ReviewService> _logger;

    public ReviewService(
        IReviewRepository reviewRepository,
        ILoggerAdapter<ReviewService> logger)
    {
        _reviewRepository = reviewRepository;
        _logger = logger;
    }

    public async Task<ReviewDto?> GetReviewById(int id, CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching review with id {ReviewId}", id);

        var review = await _reviewRepository.GetByIdAsync(id, ct);
        if (review == null)
        {
            _logger.LogWarning($"Review with ID {id} not found.");
            return null;
        }

        return MapToDto(review);
    }

    public async Task<IEnumerable<ReviewDto>> GetAllReviews(CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching all reviews");

        var reviews = await _reviewRepository.GetAllAsync(ct);

        return reviews.Select(MapToDto);
    }

    public async Task<ReviewDto> CreateReview(CreateReviewDto dto, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating review for user {UserId}", dto.UserId);

        var review = new ReviewEntity
        {
            Id = dto.Id,
            UserId = dto.UserId,
            ListingId = dto.ListingId,
            BookingId = dto.BookingId,
            Comment = dto.Comment,
            Rating = dto.Rating,
            Cleanliness = dto.Cleanliness,
            Accuracy = dto.Accuracy,
            CheckIn = dto.CheckIn,
            Communication = dto.Communication,
            Location = dto.Location,
            Value = dto.Value,
            Overall = dto.Overall
        };

        var reviewId = await _reviewRepository.CreateAsync(review, ct);
        review.Id = reviewId;

        _logger.LogInformation("Created review with id {ReviewId}", reviewId);

        return MapToDto(review);
    }

    public async Task<bool> DeleteReview(int id, CancellationToken ct = default)
    {
        var existingReview = await _reviewRepository.GetByIdAsync(id, ct);
        if (existingReview == null)
        {
            _logger.LogWarning($"Review with ID {id} not found.");
            return false;
        }

        await _reviewRepository.DeleteAsync(id, ct);
        return true;
    }

    private static ReviewDto MapToDto(ReviewEntity review) => new()
    {
        Id = review.Id,
        UserId = review.UserId,
        ListingId = review.ListingId,
        BookingId = review.BookingId,
        Comment = review.Comment,
        Rating = review.Rating,
        Cleanliness = review.Cleanliness,
        Accuracy = review.Accuracy,
        CheckIn = review.CheckIn,
        Communication = review.Communication,
        Location = review.Location,
        Value = review.Value,
        Overall = review.Overall,
        CreatedAt = review.CreatedAt,
    };
}
