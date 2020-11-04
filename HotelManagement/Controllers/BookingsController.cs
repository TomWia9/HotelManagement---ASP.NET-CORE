using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IDbRepository _dbRepository;
        private readonly IMapper _mapper;

        public BookingsController(IBookingsRepository bookingsRepository, IClientsRepository clientsRepository, IDbRepository dbRepository, IMapper mapper)
        {
            _bookingsRepository = bookingsRepository;
            _clientsRepository = clientsRepository;
            _dbRepository = dbRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDTO>> NewBooking(BookingForCreationDTO booking)
        {
            try
            {
                if (!await _bookingsRepository.IsRoomVacancyAsync(booking.RoomId, booking.BookingDates))
                {
                    return Conflict(); 
                }

                Booking newBooking = _mapper.Map<Booking>(booking);

                _dbRepository.Add(newBooking);
              
                if (await _dbRepository.SaveChangesAsync())
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
                var booking = await _bookingsRepository.GetBookingAsync(bookingId);
                if (booking != null)
                {
                    return Ok(_mapper.Map<BookingDTO>(booking));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetBookings([FromQuery] BookingsResourceParameters bookingsResourceParameters)
        {
            try
            {
                var bookings = await _bookingsRepository.GetBookingsAsync(bookingsResourceParameters);
                if (bookings != null && bookings.Any())
                {
                    return Ok(_mapper.Map<IEnumerable<BookingDTO>>(bookings));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                var bookingToRemove = await _bookingsRepository.GetBookingAsync(bookingId);
                if(bookingToRemove == null)
                {
                    return NotFound();
                }
                _dbRepository.Remove(bookingToRemove);
                if (await _dbRepository.SaveChangesAsync())
                {
                   return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, BookingForUpdateDTO booking)
        {
            try
            {
                var bookingFromRepo = await _bookingsRepository.GetBookingAsync(bookingId);

                if (bookingFromRepo == null)
                {
                    return NotFound();
                }

                if (!await _bookingsRepository.IsRoomVacancyAsync(booking.RoomId, booking.BookingDates, bookingId))
                {
                    return Conflict();
                }

                _mapper.Map(booking, bookingFromRepo);
                _bookingsRepository.UpdateBooking(bookingFromRepo);

                if (await _dbRepository.SaveChangesAsync())
                {
                    return NoContent();
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
