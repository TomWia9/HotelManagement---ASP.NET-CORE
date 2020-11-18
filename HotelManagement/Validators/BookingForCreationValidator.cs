using HotelManagement.Data.DTO;
using HotelManagement.Services;

namespace HotelManagement.Validators
{
    public class BookingForCreationValidator : BookingValidator<BookingForCreationDTO>
    {
        public BookingForCreationValidator(IClientsRepository clientsRepository, IRoomsRepository roomsRepository, IBookingsRepository bookingsRepository) : base(clientsRepository, roomsRepository, bookingsRepository)
        {

        }
    }
}
