﻿using System;
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
        public async Task<ActionResult<BookingDTO>> NewBooking(NewBookingDTO booking)
        {
            try
            {
                if(!await _clientsRepository.IsClientExists(booking.ClientId)
                    || !await _bookingsRepository.IsRoomExistsAsync(booking.RoomId))
                {
                    return BadRequest(); 
                }

                if (!_bookingsRepository.AreDatesCorrect(_mapper.Map<DatesDTO>(booking))
                    || !await _bookingsRepository.IsRoomVacancyAsync(booking.RoomId, _mapper.Map<DatesDTO>(booking))
                    )
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

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingsRepository.GetAllBookingsAsync();
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
                var bookings = await _bookingsRepository.GetCurrentBookingsAsync();
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
                if (await _bookingsRepository.IsBookingExistsAsync(bookingId))
                {
                    var bookingToRemove = await _bookingsRepository.GetBookingAsync(bookingId);
                    _dbRepository.Remove(bookingToRemove);
                    if (await _dbRepository.SaveChangesAsync())
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
                if (await _bookingsRepository.IsBookingExistsAsync(bookingId) && _bookingsRepository.AreDatesCorrect(newDates))
                {

                    if (await _bookingsRepository.EditBookingDatesAsync(bookingId, newDates))
                    {
                        if (await _dbRepository.SaveChangesAsync())
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
