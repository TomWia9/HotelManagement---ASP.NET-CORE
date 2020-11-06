﻿using FluentValidation;
using HotelManagement.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public abstract class RoomValidator<T> : AbstractValidator<T> where T : RoomForManipulationDTO
    {
        public RoomValidator()
        {
            RuleFor(room => room.Balcony).NotEmpty();
            RuleFor(room => room.Type).NotNull().IsInEnum();
            RuleFor(room => room.Description).NotEmpty().Length(10, 250);
            RuleFor(room => room.PriceForDay).NotEmpty().GreaterThan(0);
            RuleFor(room => room.MaxNumberOfPerson).NotEmpty().GreaterThan(0);
        }
    }
}
