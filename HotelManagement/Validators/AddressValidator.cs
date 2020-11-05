using FluentValidation;
using HotelManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public class AddressValidator : AbstractValidator<AddressDTO>
    {
        public AddressValidator()
        {
            RuleFor(address => address.City).NotEmpty().MaximumLength(50).MatchName();
            RuleFor(address => address.Street).MaximumLength(50).MatchName();
            RuleFor(address => address.HouseNumber).NotEmpty().MaximumLength(8).Must(h => h.All(char.IsLetterOrDigit)).WithMessage("The house number contains invalid characters.");
            RuleFor(address => address.PostCode).NotEmpty().Length(3,6).Must(BeAValidPostCode).WithMessage("The post code contains invalid characters.");
        }

        protected bool BeAValidPostCode(string postCode)
        {
            postCode = postCode.Replace("-", "");
            return postCode.All(char.IsDigit);
        }
    }
}
