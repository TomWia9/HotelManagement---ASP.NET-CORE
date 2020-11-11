using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using HotelManagement.Services;
using HotelManagement.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Authorize]
    [Produces("application/json", "application/xml")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private readonly IDbRepository _dbRepository;
        private readonly IAdminsRepository _adminsRepository;
        private readonly IMapper _mapper;

        public AdministratorsController(IDbRepository dbRepository, IAdminsRepository adminsRepository, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _adminsRepository = adminsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new administrator
        /// </summary>
        /// <param name="admin">The administrator to create</param>
        /// <returns>An ActionResult</returns>
        ///<response code="201">Creates and returns the created administrator</response>

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(AdminForCreationDTO admin)
        {
            try
            {
                if(!await _adminsRepository.IsEmailFree(admin.Email))
                {
                    return Conflict();
                }

                var newAdmin = _mapper.Map<Admin>(admin);
                newAdmin.Password = Hash.GetHash(admin.Password);

                _dbRepository.Add(newAdmin);

                if (await _dbRepository.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetAdmin), new { adminId = newAdmin.Id }, _mapper.Map<AdminDTO>(newAdmin));
                }


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        /// <summary>
        /// Get an administrator by his/her id
        /// </summary>
        /// <param name="adminId">The id of the administrator you want to get</param>
        /// <returns>An ActionResult of type AdminDTO></returns>
        /// <response code="200">Returns the requested administrator</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{adminId}")]
        public async Task<ActionResult<AdminDTO>> GetAdmin(int adminId)
        {
            try
            {
                var admin = await _adminsRepository.GetAdminAsync(adminId);
                if (admin == null)
                {
                    return Ok(_mapper.Map<AdminDTO>(admin));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        /// <summary>
        /// Get a list of administrators
        /// </summary>
        /// <param name="adminsResourceParameters">Query parameters to apply</param>
        /// <returns>An ActionResult of type IEnumerable of AdminDTO</returns>
        /// <response code="200">Returns the requested list of administrators</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDTO>>> GetAdmins([FromQuery] AdminsResourceParameters adminsResourceParameters)
        {
            try
            {
                var admins = await _adminsRepository.GetAdminsAsync(adminsResourceParameters);
                if (admins != null)
                {
                    return Ok(_mapper.Map<IEnumerable<AdminDTO>>(admins));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        /// <summary>
        /// Delete the administrator with given id
        /// </summary>
        /// <param name="adminId">The id of the administrator you want to delete</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{adminId}")]
        public async Task<IActionResult> RemoveAdmin(int adminId)
        {
            try
            {
                var adminToRemove = await _adminsRepository.GetAdminAsync(adminId);

                if (adminToRemove == null)
                {
                    return NotFound();
                }

                _dbRepository.Remove(adminToRemove);
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
        /// Update the administrator
        /// </summary>
        /// <param name="adminId">The id of the administrator to update</param>
        /// <param name="admin">The administrator with updated values</param>
        /// <returns>An IActionResult</returns>
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{adminId}")]
        public async Task<IActionResult> UpdateAdmin(int adminId, AdminForUpdateDTO admin)
        {
            try
            {
                var adminFromRepo = await _adminsRepository.GetAdminAsync(adminId);

                if(! await _adminsRepository.IsEmailFree(admin.Email) && admin.Email != adminFromRepo.Email)
                {
                    return Conflict();
                }

                if (adminFromRepo == null)
                {
                    return NotFound();
                }

                _mapper.Map(admin, adminFromRepo);
                adminFromRepo.Password = Hash.GetHash(admin.Password);
                _adminsRepository.UpdateAdmin(adminFromRepo);

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
