using System;

namespace HotelManagement.Data.DTO
{
    /// <summary>
    /// The dates of booking
    /// </summary>
    public class DatesDTO
    {
        /// <summary>
        /// The check in date of the booking
        /// </summary>
        public DateTime CheckInDate { get; set; }
        /// <summary>
        /// The check out date of the booking
        /// </summary>
        public DateTime CheckOutDate { get; set; }
    }
}
