using AccountManager.Dto.Concrete;
using FluentValidation;

namespace AccountManager.Business.ValidationRules.FluentValidation
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(a => a.UserName)
                .NotNull()
                .NotEmpty();
            RuleFor(a => a.UserName)
                .MaximumLength(50);

            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(a => a.UserName)
                .MaximumLength(50);

            RuleFor(a => a.Email)
                .NotNull()
                .NotEmpty();
            RuleFor(a => a.Email)
                .MaximumLength(50);
            RuleFor(a => a.Email)
                .EmailAddress();

            RuleFor(l => l.Password)
                .NotNull()
                .NotEmpty();
            RuleFor(l => l.Password)
                .MinimumLength(3);
        }
    }
}
