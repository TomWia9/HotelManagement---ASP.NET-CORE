using System;
using System.Collections;
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
        private readonly IClientsService _clientsService;
        private readonly IDbContextService _dbContextService;
        private readonly IMapper _mapper;

        public BookingController(HotelManagementContext context, IMapper mapper)
        {
            _bookingService = new BookingService(context);
            _clientsService = new ClientsService(context, mapper);
            _dbContextService = new DbContextService(context);
            _mapper = mapper;
        }

        [HttpPost("NewBooking")]
        public async Task<ActionResult<BookingDto>> NewBooking(NewBookingDto booking)
        {
            try
            {
                if(!await _clientsService.CheckIfClientExists(booking.ClientId))
                {
                    return BadRequest(); //There is no such client
                }

                if (!await _bookingService.CheckIfRoomExistsAsync(booking.RoomId))
                {
                    return BadRequest(); //There is no such room
                }

                if(!await _bookingService.CheckIfRoomIsVacancyAsync(booking.RoomId))
                {
                    return Conflict(); //Room is not vacancy
                }

                Booking newBooking = _mapper.Map<Booking>(booking);

                _dbContextService.Add(newBooking);
              
                if (await _bookingService.ChangeRoomVacancyStatusAsync(booking.RoomId) && await _dbContextService.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetBooking), new { bookingId = newBooking.Id }, _mapper.Map<BookingDto>(newBooking));
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpGet("GetBooking/{bookingId}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int bookingId)
        {
            try
            {
                var booking = await _bookingService.GetBookingAsync(bookingId);
                if (booking != null)
                {
                    return Ok(_mapper.Map<BookingDto>(booking));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<IEnumerable>> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookingsAsync();
                if (bookings != null)
                {
                    return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("RemoveBooking/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                if (await _bookingService.CheckIfBookingExistsAsync(bookingId))
                {
                    var bookingToRemove = await _bookingService.GetBookingAsync(bookingId);
                    _dbContextService.Remove(bookingToRemove);
                    if (await _bookingService.ChangeRoomVacancyStatusAsync(bookingToRemove.RoomId) && await _dbContextService.SaveChangesAsync())
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


    }
}
