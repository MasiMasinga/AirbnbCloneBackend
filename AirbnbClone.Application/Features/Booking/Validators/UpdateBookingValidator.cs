using FluentValidation;
using AirbnbClone.Application.Features.Booking.DTOs;

namespace AirbnbClone.Application.Features.Booking.Validators;

public class UpdateBookingValidator : AbstractValidator<UpdateBookingDto>
{
    public UpdateBookingValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ListingId).GreaterThan(0);
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.NumberOfGuests).GreaterThan(0);
    }
}