using FluentValidation;
using HotelManagement.Data.DTO;

namespace HotelManagement.Validators
{
    public abstract class AdminValidator<T> : AbstractValidator<T> where T : AdminForManipulationDTO
    {
        public AdminValidator()
        {
            RuleFor(admin => admin.Email)
              .Cascade(CascadeMode.Stop)
              .NotEmpty()
              .EmailAddress()
              .MaximumLength(30);

            RuleFor(admin => admin.FirstName)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MaximumLength(25);

            RuleFor(admin => admin.LastName)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MaximumLength(25);

            RuleFor(admin => admin.Password)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MaximumLength(32);
        }
    }
}
