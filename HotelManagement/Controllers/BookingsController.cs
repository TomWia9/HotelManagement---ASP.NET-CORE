using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.DTO;
using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IClientsService _clientsService;
        private readonly IDbContextService _dbContextService;
        private readonly IMapper _mapper;

        public BookingsController(DatabaseContext context, IMapper mapper)
        {
            _bookingService = new BookingService(context);
            _clientsService = new ClientsService(context);
            _dbContextService = new DbContextService(context);
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDTO>> NewBooking(NewBookingDTO booking)
        {
            try
            {
                if(!await _clientsService.IsClientExists(booking.ClientId)
                    || !await _bookingService.IsRoomExistsAsync(booking.RoomId))
                {
                    return BadRequest(); 
                }

                if (!_bookingService.AreDatesCorrect(_mapper.Map<DatesDTO>(booking))
                    || !await _bookingService.IsRoomVacancyAsync(booking.RoomId, _mapper.Map<DatesDTO>(booking))
                    )
                {
                    return Conflict(); 
                }

                Booking newBooking = _mapper.Map<Booking>(booking);

                _dbContextService.Add(newBooking);
              
                if (await _dbContextService.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetBooking), new { bookingId = newBooking.Id }, _mapper.Map<BookingDTO>(newBooking));
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpGet("{bookingId}")]
        public async Task<ActionResult<BookingDTO>> GetBooking(int bookingId)
        {
            try
            {
                var booking = await _bookingService.GetBookingAsync(bookingId);
                if (booking != null)
                {
                    return Ok(_mapper.Map<BookingDTO>(booking));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookingsAsync();
                if (bookings != null)
                {
                    return Ok(_mapper.Map<IEnumerable<BookingDTO>>(bookings));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpGet("CurrentBookings")]//
        public async Task<ActionResult<IEnumerable>> GetCurrentBookings()
        {
            try
            {
                var bookings = await _bookingService.GetCurrentBookingsAsync();
                if (bookings != null)
                {
                    return Ok(_mapper.Map<IEnumerable<BookingDTO>>(bookings));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                if (await _bookingService.IsBookingExistsAsync(bookingId))
                {
                    var bookingToRemove = await _bookingService.GetBookingAsync(bookingId);
                    _dbContextService.Remove(bookingToRemove);
                    if (await _dbContextService.SaveChangesAsync())
                    {
                        return Ok();
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPatch("{bookingId}")]
        public async Task<IActionResult> EditBookingDates(int bookingId, DatesDTO newDates)
        {
            try
            {
                if (await _bookingService.IsBookingExistsAsync(bookingId) && _bookingService.AreDatesCorrect(newDates))
                {

                    if (await _bookingService.EditBookingDatesAsync(bookingId, newDates))
                    {
                        if (await _dbContextService.SaveChangesAsync())
                        {
                            return NoContent();
                        }
                    }

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
