using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using HotelManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IDbRepository _dbRepository;

        public RoomsController(IMapper mapper, IRoomsRepository roomsRepository, IDbRepository dbRepository)
        {
            _mapper = mapper;
            _roomsRepository = roomsRepository;
            _dbRepository = dbRepository;
        }

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="room">The room to create</param>
        /// <returns>An ActionResult of type RoomDTO</returns>
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> NewRoom(RoomForCreationDTO room)
        {
            try
            {
                var newRoom = _mapper.Map<Room>(room);

                _dbRepository.Add(newRoom);

                if (await _dbRepository.SaveChangesAsync())
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

        /// <summary>
        /// Get a room by id
        /// </summary>
        /// <param name="roomId">The id of the room you want to get</param>
        /// <returns>An ActionResult of type RoomDTO</returns>
        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int roomId)
        {
            try
            {
                var room = await _roomsRepository.GetRoomAsync(roomId);
                if (room != null)
                {
                    return Ok(_mapper.Map<RoomDTO>(room));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        /// <summary>
        /// Get a list of rooms
        /// </summary>
        /// <param name="roomsResourceParameters">Query parametersto apply</param>
        /// <returns>An ActionResult of type IEnumerable of RoomDTO</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms([FromQuery] RoomsResourceParameters roomsResourceParameters)
        {
            try
            {
                var rooms = await _roomsRepository.GetRoomsAsync(roomsResourceParameters);
                if (rooms != null)
                {
                    return Ok(_mapper.Map<IEnumerable<RoomDTO>>(rooms));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        /// <summary>
        /// Delete the room with given id
        /// </summary>
        /// <param name="roomId">The id of the room you want to remove</param>
        /// <returns>An IActionResult</returns>
        [HttpDelete("{roomId}")]
        public async Task<IActionResult> RemoveRoom(int roomId)
        {
            try
            {
                var bookingToRemove = await _roomsRepository.GetRoomAsync(roomId);
                if(bookingToRemove == null)
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
        /// Update the room
        /// </summary>
        /// <param name="roomId">The id of the room to update</param>
        /// <param name="room">The room with updated values</param>
        /// <returns>An IActionResult</returns>
        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoom(int roomId, RoomForUpdateDTO room)
        {
            try
            {
                var roomFromRepo = await _roomsRepository.GetRoomAsync(roomId);

                if (roomFromRepo == null)
                {
                    return NotFound();
                }

                _mapper.Map(room, roomFromRepo);
                _roomsRepository.UpdateRoom(roomFromRepo);
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
        /// Partially update a room
        /// </summary>
        /// <param name="roomId">The id of the room you want to get</param>
        /// <param name="patchDocument">The set of operations to apply to the room</param>
        /// <returns>An IActionResult</returns>
        /// <remarks>
        /// Sample request (this request updates the rooms's description) \
        /// PATCH /clients/id \
        /// [ \
        ///     { \
        ///         "op": "replace", \
        ///         "patch": "/description", \
        ///         "value": "new description" \
        ///     } \
        /// ] 
        /// </remarks>
        [HttpPatch("{roomId}")]
        public async Task<IActionResult> PartiallyUpdateRoom(int roomId, JsonPatchDocument<RoomForUpdateDTO> patchDocument)
        {
            try
            {
                var roomFromRepo = await _roomsRepository.GetRoomAsync(roomId);

                if (roomFromRepo == null)
                {
                    return NotFound();
                }

                var roomToPatch = _mapper.Map<RoomForUpdateDTO>(roomFromRepo);
                patchDocument.ApplyTo(roomToPatch, ModelState);
                _mapper.Map(roomToPatch, roomFromRepo);
                _roomsRepository.UpdateRoom(roomFromRepo);

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
