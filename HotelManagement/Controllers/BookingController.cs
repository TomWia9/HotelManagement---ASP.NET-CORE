using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Data.Dto;
using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(HotelManagementContext context, IMapper mapper)
        {
            _bookingService = new BookingService(context);
            _mapper = mapper;
        }

        [HttpPost("NewBooking")]
        public async Task<IActionResult> NewBooking(BookingDto booking)
        {
            try
            {
                if (!await _bookingService.CheckIfRoomExists(booking.RoomId))
                {
                    return BadRequest();
                }

                if(!await _bookingService.CheckIfRoomIsVacancy(booking.RoomId))
                {
                    return Conflict();
                }

                Booking newBooking = _mapper.Map<Booking>(booking);

                _bookingService.Add(newBooking);
              
                if (await _bookingService.ChangeRoomVacancyStatus(booking.RoomId) && await _bookingService.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }


    }
}
