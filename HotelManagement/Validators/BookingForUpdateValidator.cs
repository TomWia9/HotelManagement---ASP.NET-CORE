using FluentValidation;
using HotelManagement.Data.DTO;
using HotelManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public class BookingForUpdateValidator : AbstractValidator<BookingForUpdateDTO>
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IBookingsRepository _bookingsRepository;
        public BookingForUpdateValidator(IClientsRepository clientsRepository, IRoomsRepository roomsRepository, IBookingsRepository bookingsRepository)
        {
            _clientsRepository = clientsRepository;
            _roomsRepository = roomsRepository;
            _bookingsRepository = bookingsRepository;

            RuleFor(booking => booking.ClientId)
                .NotNull()
                .MustAsync(async (clientId, cancellation) => { return await _clientsRepository.IsClientExistsAsync(clientId); })
                .WithMessage("Client with this id doesn't exists.");

            RuleFor(booking => booking.RoomId)
               .NotNull()
               .MustAsync(async (roomId, cancellation) => { return await _roomsRepository.IsRoomExistsAsync(roomId); })
               .WithMessage("Room with this id doesn't exists.");

            RuleFor(booking => booking.BookingDates)
               .NotNull()
               .Must(BeAValidDates)
               .WithMessage("Dates are incorrect");

            RuleFor(booking => booking.NumberOfPerson)
                .NotNull()
                .LessThanOrEqualTo(booking => GetMaxNumberOfPersonInRoom(booking.RoomId));
           

        }

        protected bool BeAValidDates(DatesDTO dates)
        {
            return _bookingsRepository.AreDatesCorrect(dates);
        }

        protected int Xd(int xd)
        {
            return 2;
        }

        protected int GetMaxNumberOfPersonInRoom(int roomId)
        {
            var room = _roomsRepository.GetRoomAsync(roomId).Result;
            return room.MaxNumberOfPerson;
        }
    }
}
