using FluentValidation;
using AirbnbClone.Application.Features.Authentication.DTOs;

namespace AirbnbClone.Application.Features.Authentication.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.UserRole).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(100);
        RuleFor(x => x.EmailAddress).NotEmpty().MaximumLength(100).EmailAddress();
        RuleFor(x => x.Bio).NotEmpty().MaximumLength(360);
        RuleFor(x => x.Photo).NotEmpty();
    }
}