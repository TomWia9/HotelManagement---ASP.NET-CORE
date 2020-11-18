using AutoMapper;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IMapper _mapper;

        public StatisticsController(IMapper mapper, IStatisticsService statisticsService)
        {
            _mapper = mapper;
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Get a list of the most popular rooms
        /// </summary>
        /// <param name="numberOfRooms">Query parameter to define number of returned rooms</param>
        /// <returns>An ActionResult of type IEnumerable of RoomDTO</returns>
        /// <response code="200">Returns the requested list of the most popular rooms</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("rooms")]
        public async Task<ActionResult<IEnumerable>> GetRoomsByPopularity([FromQuery] uint? numberOfRooms)
        {
            try
            {
                var rooms = await _statisticsService.GetRoomsByPopularityAsync(numberOfRooms);
                if (rooms != null)
                {
                    return Ok(rooms);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        /// <summary>
        /// Get a list of the clients who made the most bookings
        /// </summary>
        /// <param name="numberOfClients">Query parameter to define number of returned clients</param>
        /// <returns>An ActionResult of type IEnumerable</returns>
        /// <response code="200">Returns the requested list of the clients who made the most bookings</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("clients")]
        public async Task<ActionResult<IEnumerable>> GetClientsByBookingsNumbers([FromQuery] uint? numberOfClients)
        {
            try
            {
                var clients = await _statisticsService.GetClientsByBookingsNumbersAsync(numberOfClients);
                if (clients != null)
                {
                    return Ok(clients);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        /// <summary>
        /// Get a total earned money from bookings from peroid defined in queryParameter
        /// </summary>
        /// <param name="peroid">Query parameter to define peroid of earned money</param>
        /// <returns>An ActionResult of type decimal</returns>
        /// <response code="200">Returns the requested total earned money</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("money")]
        public async Task<ActionResult<decimal>> GetTotalEarnedMoney([FromQuery] uint? peroid)
        {
            try
            {
                decimal? totalMoney = await _statisticsService.GetTotalEarnedMoneyAsync(peroid);
                if (totalMoney != null)
                {
                    return Ok(totalMoney);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }
    }
}
