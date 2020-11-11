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
        [HttpPost]
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
                    return Ok();
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
        [HttpGet("{adminId}")]
        public async Task<ActionResult<AdminDTO>> GetAdmin(int adminId)
        {
            try
            {
                var admin = await _adminsRepository.GetAdminAsync(adminId);
                if (admin != null)
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
