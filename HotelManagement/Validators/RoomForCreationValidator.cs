using FluentValidation;
using HotelManagement.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public class RoomForCreation : AbstractValidator<RoomForCreationDTO>
    {
        public RoomForCreation()
        {
            RuleFor(room => room.Balcony).NotNull();
            RuleFor(room => room.Type).NotNull().IsInEnum();
            RuleFor(room => room.Description).NotEmpty().Length(10, 250);

        }
    }
}
