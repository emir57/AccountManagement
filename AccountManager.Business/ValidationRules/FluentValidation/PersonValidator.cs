using AccountManager.Dto.Concrete;
using FluentValidation;

namespace AccountManager.Business.ValidationRules.FluentValidation
{
    public class PersonValidator : AbstractValidator<PersonDto>
    {
        public PersonValidator()
        {
            RuleFor(p => p.FirstName)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.FirstName)
                .MaximumLength(50);

            RuleFor(p => p.LastName)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.LastName)
                .MaximumLength(50);

            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Email)
                .MaximumLength(50);

            RuleFor(p => p.Description)
                .MaximumLength(250);

            RuleFor(p => p.Phone)
                .MaximumLength(11);

            RuleFor(p => p.DateOfBirth)
                .NotNull()
                .NotEmpty();
        }
    }
}
