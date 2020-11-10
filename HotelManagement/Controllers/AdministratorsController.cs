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

        [HttpPost]
        public async Task<IActionResult> Register(AdminForCreationDTO admin)
        {
            try
            {
                var newAdmin = _mapper.Map<Admin>(admin);
                newAdmin.Password = Hash.GetHash(admin.Password);

                _dbRepository.Add(newAdmin);

                if (await _dbRepository.SaveChangesAsync())
                {
                    //return Ok(token);
                    return Ok();
                }


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }


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

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetAdmins([FromQuery] AdminsResourceParameters adminsResourceParameters)
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


    }
}
