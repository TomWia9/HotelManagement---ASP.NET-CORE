using FluentValidation;
using HotelManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public class ClientValidator : AbstractValidator<NewClientDTO>
    {
        public ClientValidator()
        {
            RuleFor(client => client.FirstName).Cascade(CascadeMode.Stop).NotEmpty().Length(2, 25).MatchName();
            RuleFor(client => client.Sex).NotNull().IsInEnum();
            RuleFor(client => client.LastName).Cascade(CascadeMode.Stop).NotEmpty().Length(2, 25).MatchName();
            RuleFor(client => client.Age).NotEmpty().InclusiveBetween(18,120);
            RuleFor(client => client.PhoneNumber).NotEmpty().MatchPhoneNumber();
            RuleFor(client => client.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(client => client.Address).SetValidator(new AddressValidator()).NotNull();
        }
    }
}
