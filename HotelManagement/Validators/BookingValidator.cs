using FluentValidation;
using HotelManagement.Data.DTO;
using HotelManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Validators
{
    public abstract class BookingValidator<T> : AbstractValidator<T> where T : BookingForManipulationDTO
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IBookingsRepository _bookingsRepository;
        public BookingValidator(IClientsRepository clientsRepository, IRoomsRepository roomsRepository, IBookingsRepository bookingsRepository)
        {
            _clientsRepository = clientsRepository;
            _roomsRepository = roomsRepository;
            _bookingsRepository = bookingsRepository;

            RuleFor(booking => booking.ClientId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MustAsync(async (clientId, cancellation) => { return await _clientsRepository.IsClientExistsAsync(clientId); })
                .WithMessage("Client with this id doesn't exists.");

            RuleFor(booking => booking.RoomId)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MustAsync(async (roomId, cancellation) => { return await _roomsRepository.IsRoomExistsAsync(roomId); })
               .WithMessage("Room with this id doesn't exists.");

            RuleFor(booking => booking.BookingDates)
               .NotNull()
               .Must(BeAValidDates)
               .WithMessage("Dates are incorrect");

            RuleFor(booking => booking.NumberOfPerson)
                .NotEmpty()
                .LessThanOrEqualTo(booking => GetMaxNumberOfPersonInRoom(booking.RoomId))
                .GreaterThan(0);

        }

        protected bool BeAValidDates(DatesDTO dates)
        {
            return _bookingsRepository.AreDatesCorrect(dates);
        }

        protected int GetMaxNumberOfPersonInRoom(int roomId)
        {
            var room = _roomsRepository.GetRoomAsync(roomId).Result;
            if (room == null)
            {
                return 0;
            }
            return room.MaxNumberOfPerson;
        }
    }
}
