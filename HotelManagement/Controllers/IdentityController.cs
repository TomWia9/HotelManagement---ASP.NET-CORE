using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Data.Auth;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.Services;
using HotelManagement.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IDbRepository _dbRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public IdentityController(IDbRepository dbRepository, IMapper mapper, IIdentityRepository identityRepository)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
            _identityRepository = identityRepository;
        }


        [Authorize]
        [HttpPost("Register")]
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

        [HttpPost("Authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticateRequest authenticateRequest)
        {
            try
            {
                var response = await _identityRepository.Authenticate(authenticateRequest);

                if(response == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });

                }

                return Ok(response);

            }
            catch (Exception)
            {
                 return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }
    }
}
