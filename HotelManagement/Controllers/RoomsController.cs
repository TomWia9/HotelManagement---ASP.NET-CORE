using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using HotelManagement.Services;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetRooms([FromQuery] RoomsResourceParameters roomsResourceParameters)
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
