using AccountManager.Dto.Concrete;
using FluentValidation;

namespace AccountManager.Business.ValidationRules.FluentValidation
{
    public class AccountValidator : AbstractValidator<AccountDto>
    {
        public AccountValidator()
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
        }
    }
}
