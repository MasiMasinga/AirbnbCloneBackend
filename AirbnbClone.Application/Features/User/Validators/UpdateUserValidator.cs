using FluentValidation;
using AirbnbClone.Application.Features.User.DTOs;

namespace AirbnbClone.Application.Features.User.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x)
            .Must(HasAtLeastOneChange)
            .WithMessage("At least one field must be provided.");

        RuleFor(x => x.Title).NotEmpty().MaximumLength(100)
            .When(x => x.Title is not null);
        RuleFor(x => x.UserRole).NotEmpty().MaximumLength(20)
            .When(x => x.UserRole is not null);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100)
            .When(x => x.FirstName is not null);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(100)
            .When(x => x.Surname is not null);
        RuleFor(x => x.EmailAddress).NotEmpty().MaximumLength(100).EmailAddress()
            .When(x => x.EmailAddress is not null);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
            .When(x => x.Password is not null);
        RuleFor(x => x.Bio).NotEmpty().MaximumLength(360)
            .When(x => x.Bio is not null);
        RuleFor(x => x.Photo).NotEmpty()
            .When(x => x.Photo is not null);
    }

    private static bool HasAtLeastOneChange(UpdateUserDto dto) =>
        dto.Title is not null ||
        dto.UserRole is not null ||
        dto.FirstName is not null ||
        dto.Surname is not null ||
        dto.EmailAddress is not null ||
        dto.Password is not null ||
        dto.Bio is not null ||
        dto.Photo is not null;
}
