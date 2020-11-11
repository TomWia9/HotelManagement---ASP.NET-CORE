using HotelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    public abstract class RoomForManipulationDTO
    {
        /// <summary>
        /// The type of the room
        /// </summary>
        public RoomType? Type { get; set; }
        /// <summary>
        /// Room having a balcony (true or false)
        /// </summary>
        public bool Balcony { get; set; }
        /// <summary>
        /// The description of the room
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The price of room per day
        /// </summary>
        public decimal PriceForDay { get; set; }
        /// <summary>
        /// The maximum number of person for the room
        /// </summary>
        public int MaxNumberOfPerson { get; set; }


    }
}
