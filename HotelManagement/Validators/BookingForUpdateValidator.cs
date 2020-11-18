using HotelManagement.Data.DTO;
using HotelManagement.Services;

namespace HotelManagement.Validators
{
    public class BookingForUpdateValidator : BookingValidator<BookingForUpdateDTO>
    {
        public BookingForUpdateValidator(IClientsRepository clientsRepository, IRoomsRepository roomsRepository, IBookingsRepository bookingsRepository) : base(clientsRepository, roomsRepository, bookingsRepository)
        {

        }
    }
}
