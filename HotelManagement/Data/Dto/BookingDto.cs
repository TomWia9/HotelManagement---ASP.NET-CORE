using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    /// <summary>
    /// The booking with Id, CheckInDate, CheckOutDate, NumberOfPerson, ClientId, RoomId and TotalPrice fields
    /// </summary>
    public class BookingDTO
    {
        /// <summary>
        /// The id of the booking
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The check in date of the booking
        /// </summary>
        public DateTime CheckInDate { get; set; }
        /// <summary>
        /// The check out date of the booking
        /// </summary>
        public DateTime CheckOutDate { get; set; }
        /// <summary>
        /// The number of persons of the booking
        /// </summary>
        public int NumberOfPerson { get; set; }
        /// <summary>
        /// The id of the client who wants to make a boooking
        /// </summary>
        public int? ClientId { get; set; }
        /// <summary>
        /// The id of the booking room
        /// </summary>
        public int? RoomId { get; set; }
        /// <summary>
        /// Total price of the booking
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
