﻿using HotelManagement.Data.Auth;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityRepository _identityRepository;

        public IdentityController(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        /// <summary>
        /// Authenticate the admin
        /// </summary>
        /// <param name="authenticateRequest">An email and password of administrator</param>
        /// <returns>An ActionResult</returns>
        /// <response code="200">Returns the administrator with token</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest authenticateRequest)
        {
            try
            {
                var response = await _identityRepository.Authenticate(authenticateRequest);

                if (response == null)
                {
                    return BadRequest(new { message = "Email or password is incorrect" });

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
