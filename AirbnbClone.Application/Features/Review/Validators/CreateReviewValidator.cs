using FluentValidation;
using AirbnbClone.Application.Features.Review.DTOs;

namespace AirbnbClone.Application.Features.Review.Validators;

public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.BookingId).NotEmpty();
        RuleFor(x => x.Comment).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Rating).GreaterThanOrEqualTo(0).WithMessage("Rating cannot be negative.").LessThanOrEqualTo(100).WithMessage("Rating cannot exceed 5.");
        RuleFor(x => x.Cleanliness).GreaterThanOrEqualTo(0).WithMessage("Cleanliness cannot be negative.").LessThanOrEqualTo(100).WithMessage("Cleanliness cannot exceed 5.");
        RuleFor(x => x.Accuracy).GreaterThanOrEqualTo(0).WithMessage("Accuracy cannot be negative.").LessThanOrEqualTo(100).WithMessage("Accuracy cannot exceed 5.");
        RuleFor(x => x.CheckIn).GreaterThanOrEqualTo(0).WithMessage("CheckIn cannot be negative.").LessThanOrEqualTo(100).WithMessage("CheckIn cannot exceed 5.");
        RuleFor(x => x.Communication).GreaterThanOrEqualTo(0).WithMessage("Communication cannot be negative.").LessThanOrEqualTo(100).WithMessage("Communication cannot exceed 5.");
        RuleFor(x => x.Location).GreaterThanOrEqualTo(0).WithMessage("Location cannot be negative.").LessThanOrEqualTo(100).WithMessage("Location cannot exceed 5.");
        RuleFor(x => x.Value).GreaterThanOrEqualTo(0).WithMessage("Value cannot be negative.").LessThanOrEqualTo(100).WithMessage("Value cannot exceed 5.");
        RuleFor(x => x.Overall).GreaterThanOrEqualTo(0).WithMessage("Overall cannot be negative.").LessThanOrEqualTo(100).WithMessage("Overall cannot exceed 5.");
    }
}