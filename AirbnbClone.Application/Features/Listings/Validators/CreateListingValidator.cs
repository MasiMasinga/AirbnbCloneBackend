using FluentValidation;
using AirbnbClone.Application.Features.Listings.DTOs;

namespace AirbnbClone.Application.Features.Listings.Validators;

public class CreateListingValidator : AbstractValidator<CreateListingDto>
{
    public CreateListingValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(140);

        RuleFor(x => x.Amenities)
            .NotEmpty()
            .MaximumLength(140);

        RuleFor(x => x.HouseRules)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Pricing)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.BedCount)
            .GreaterThan(0);

        RuleFor(x => x.BathCount)
            .GreaterThan(0);

        RuleFor(x => x.PropertyType)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Photo)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date.");
    }
}