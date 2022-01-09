using FluentValidation;
using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {

        public CustomerValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Username must not be a empty.")
                .MaximumLength(25).WithMessage("Username must be a maximum of 25 characters.")
                .MinimumLength(2).WithMessage("Username must be a minimum 2 characters.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname must not be a empty.")
                .MaximumLength(25).WithMessage("Surname must be a maximum of 25 characters.")
                .MinimumLength(2).WithMessage("Surname must be a minimum 2 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail must not be a empty.")
                .EmailAddress().WithMessage("Enter the e-mail address in the correct format.");

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");

        }

    }
}
