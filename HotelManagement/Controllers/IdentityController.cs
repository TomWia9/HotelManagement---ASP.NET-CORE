﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Data.DTO;
using HotelManagement.Models;
using HotelManagement.Services;
using HotelManagement.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IDbRepository _dbRepository;
        private readonly IMapper _mapper;

        public IdentityController(IDbRepository dbRepository, IMapper mapper)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
        }

        
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
                // return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                throw;
            }

            return BadRequest();
        }
    }
}