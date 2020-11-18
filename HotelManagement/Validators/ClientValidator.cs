using FluentValidation;
using HotelManagement.Data.DTO;

namespace HotelManagement.Validators
{
    public abstract class ClientValidator<T> : AbstractValidator<T> where T : ClientForManipulationDTO
    {
        public ClientValidator()
        {
            RuleFor(client => client.FirstName).Cascade(CascadeMode.Stop).NotEmpty().Length(2, 25).MatchName();
            RuleFor(client => client.Sex).NotNull().IsInEnum();
            RuleFor(client => client.LastName).Cascade(CascadeMode.Stop).NotEmpty().Length(2, 25).MatchName();
            RuleFor(client => client.Age).NotEmpty().InclusiveBetween(18, 120);
            RuleFor(client => client.PhoneNumber).Cascade(CascadeMode.Stop).NotEmpty().MatchPhoneNumber();
            RuleFor(client => client.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(client => client.Address).SetValidator(new AddressValidator()).NotNull();
        }
    }
}
