using FluentValidation;
using HotelManagement.Data.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(admin => admin.Email)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .EmailAddress()
               .MaximumLength(30);

            RuleFor(admin => admin.Password)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MaximumLength(32);

        }
    }
}
