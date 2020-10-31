﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.DTO;
using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IClientsService _clientsService;
        private readonly IDbContextService _dbContextService;
        private readonly IMapper _mapper;

        public ClientsController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _clientsService = new ClientsService(context);
            _dbContextService = new DbContextService(context);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> NewClient(NewClientDTO client)
        {
            try
            {
                var newClient = _mapper.Map<Client>(client);

                _dbContextService.Add(newClient);

                if (await _dbContextService.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(GetClient), new { clientId = newClient.Id }, _mapper.Map<ClientDTO>(newClient));
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();

        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<ClientDTO>> GetClient(int clientId)
        {
            try
            {
                var client = await _clientsService.GetClientAsync(clientId);
                if (client != null)
                {
                    return Ok(_mapper.Map<ClientDTO>(client));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetAllClients()
        {
            try
            {
                var clients = await _clientsService.GetAllClientsAsync();
                if (clients != null)
                {
                    return Ok(_mapper.Map<IEnumerable<ClientDTO>>(clients));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{clientId}")]
        public async Task<IActionResult> RemoveClient(int clientId)
        {
            try
            {
                if (await _clientsService.IsClientExists(clientId))
                {
                    var clientToRemove = await _clientsService.GetClientAsync(clientId);
                    _dbContextService.Remove(clientToRemove);
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
        public async Task<IActionResult> UpdateClientData(ClientDTO client)
        {
            try
            {
                if (client != null && await _clientsService.IsClientExists(client.Id))
                {
                    
                    if (await _clientsService.UpdateClientData(client))
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
