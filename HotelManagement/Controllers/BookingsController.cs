using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using HotelManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
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

        /// <summary>
        /// Create a new booking
        /// </summary>
        /// <param name="booking">The booking to create</param>
        /// <returns>An ActionResult of type BookingDTO</returns>
        /// <response code="201">Creates and returns the created booking</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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
                newBooking.TotalPrice = await _bookingsRepository.CalculateTotalPrice(booking.RoomId, (int)booking.NumberOfPerson, booking.BookingDates);
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

        /// <summary>
        /// Get a booking by id
        /// </summary>
        /// <param name="bookingId">The id of the booking you want to get</param>
        /// <returns>An ActionResult of type BookingDTO</returns>
        /// <response code="200">Returns the requested booking</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Get a list of bookings
        /// </summary>
        /// <param name="bookingsResourceParameters">Query paramaters to apply</param>
        /// <returns>An ActionResult of type IEnumerable of BookingDTO</returns>
        /// <response code="200">Returns the requested list of bookings</response>

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings([FromQuery] BookingsResourceParameters bookingsResourceParameters)
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

        /// <summary>
        /// Delete the booking with given id
        /// </summary>
        /// <param name="bookingId">The id of the booking you want to delete</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                var bookingToRemove = await _bookingsRepository.GetBookingAsync(bookingId);
                if (bookingToRemove == null)
                {
                    return NotFound();
                }
                _dbRepository.Remove(bookingToRemove);
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

        /// <summary>
        /// Update the booking
        /// </summary>
        /// <param name="bookingId">The id of the booking to update</param>
        /// <param name="booking">The booking with updated values</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
                bookingFromRepo.TotalPrice = await _bookingsRepository.CalculateTotalPrice(booking.RoomId, (int)booking.NumberOfPerson, booking.BookingDates);
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
