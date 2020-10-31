using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;
        private readonly IDbContextService _dbContextService;

        public RoomsController(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _roomService = new RoomService(context);
            _dbContextService = new DbContextService(context);
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> NewRoom(NewBookingDTO room)
        {
            try
            {
                var newRoom = _mapper.Map<Room>(room);

                _dbContextService.Add(newRoom);

                if (await _dbContextService.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetRoom), new { roomId = newRoom.Id }, _mapper.Map<RoomDTO>(newRoom));
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();

        }

        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int roomId)
        {
            try
            {
                var room = await _roomService.GetRoomAsync(roomId);
                if (room != null)
                {
                    return Ok(_mapper.Map<RoomDTO>(room));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> RemoveRoom(int roomId)
        {
            try
            {
                if (await _roomService.IsRoomExistsAsync(roomId))
                {
                    var bookingToRemove = await _roomService.GetRoomAsync(roomId);
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

        [HttpPut]
        public async Task<IActionResult> UpdateRoom(RoomDTO room)
        {
            try
            {
                if (room != null && await _roomService.IsRoomExistsAsync(room.Id))
                {

                    if (await _roomService.UpdateRoomDataAsync(room))
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
