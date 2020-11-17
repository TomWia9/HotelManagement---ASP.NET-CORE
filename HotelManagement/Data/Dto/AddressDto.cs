using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Data.DTO
{
    /// <summary>
    /// An address with City, Street, HouseNumber and PostCode fields
    /// </summary>
    public class AddressDTO
    {
        /// <summary>
        /// The city 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// The street 
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// The house number 
        /// </summary>
        public string HouseNumber { get; set; }
        /// <summary>
        /// The post code 
        /// </summary>
        public string PostCode { get; set; }
    }
}
