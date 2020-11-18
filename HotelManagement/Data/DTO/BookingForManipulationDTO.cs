namespace HotelManagement.Data.DTO
{
    public abstract class BookingForManipulationDTO
    {

        public DatesDTO BookingDates { get; set; }
        /// <summary>
        /// The number of persons of the booking
        /// </summary>
        public int NumberOfPerson { get; set; }
        /// <summary>
        /// The id of the client who wants to make a boooking
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// Total price of the booking
        /// </summary>
        public int RoomId { get; set; }
    }
}
