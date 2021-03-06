﻿using FluentValidation;
using HotelManagement.Data.DTO;
using System.Linq;

namespace HotelManagement.Validators
{
    public class AddressValidator : AbstractValidator<AddressDTO>
    {
        public AddressValidator()
        {
            RuleFor(address => address.City).NotEmpty().MaximumLength(50).MatchName();
            RuleFor(address => address.Street).MaximumLength(50).MatchName();
            RuleFor(address => address.HouseNumber).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(8).Must(h => h.All(char.IsLetterOrDigit)).WithMessage("The house number contains invalid characters.");
            RuleFor(address => address.PostCode).Cascade(CascadeMode.Stop).NotEmpty().Length(3, 6).Must(BeAValidPostCode).WithMessage("The post code contains invalid characters.");
        }

        protected bool BeAValidPostCode(string postCode)
        {
            postCode = postCode.Replace("-", "");
            return postCode.All(char.IsDigit);
        }
    }
}
