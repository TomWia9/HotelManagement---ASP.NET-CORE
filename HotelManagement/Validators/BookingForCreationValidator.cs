using FluentValidation;
using HotelManagement.Data.DTO;
using HotelManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public class BookingForCreationValidator : BookingValidator<BookingForCreationDTO>
    {
        public BookingForCreationValidator(IClientsRepository clientsRepository, IRoomsRepository roomsRepository, IBookingsRepository bookingsRepository) : base(clientsRepository, roomsRepository, bookingsRepository)
        {

        }
    }
}
