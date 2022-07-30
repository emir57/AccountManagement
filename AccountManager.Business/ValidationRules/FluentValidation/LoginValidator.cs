using AccountManager.Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Business.ValidationRules.FluentValidation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(l => l.UserNameOrEmail)
                .NotNull()
                .NotEmpty();
            RuleFor(l => l.UserNameOrEmail)
                .MaximumLength(50);

            RuleFor(l => l.Password)
                .NotNull()
                .NotEmpty();
            RuleFor(l => l.Password)
                .MinimumLength(3);
        }
    }
}
