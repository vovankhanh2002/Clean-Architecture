using eComm.Application.DTOs.Identites;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComm.Application.Validation.Authentication
{
    public class CreateUserValidation : AbstractValidator<CreateUser>
    {
        public CreateUserValidation()
        {
            RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage("Full name is required.");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[^\w]").WithMessage("Password must contain at least one special character."); ;

            RuleFor(x => x.ComfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
