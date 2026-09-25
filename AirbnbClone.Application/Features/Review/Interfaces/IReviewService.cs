using AirbnbClone.Application.Features.Review.DTOs;

namespace AirbnbClone.Application.Features.Review.Interfaces;

public interface IReviewService
{
    Task<ReviewDto?> GetReviewById(int id, CancellationToken ct = default);
    Task<IEnumerable<ReviewDto>> GetAllReviews(CancellationToken ct = default);
    Task<ReviewDto> CreateReview(CreateReviewDto dto, CancellationToken ct = default);
    Task<bool> DeleteReview(int id, CancellationToken ct = default);
}