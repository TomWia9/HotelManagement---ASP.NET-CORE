using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using HotelManagement.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using HotelManagement.Services;
using HotelManagement.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IDbRepository _dbRepository;
        private readonly IMapper _mapper;

        public ClientsController(IClientsRepository clientsRepository, IDbRepository dbRepository, IMapper mapper)
        {
            _mapper = mapper;
            _clientsRepository = clientsRepository;
            _dbRepository = dbRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> NewClient(NewClientDTO client)
        {
            try
            {
                
                var newClient = _mapper.Map<Client>(client);

                _dbRepository.Add(newClient);

                if (await _dbRepository.SaveChangesAsync())
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
                var client = await _clientsRepository.GetClientAsync(clientId);
                if (client != null)
                {
                    return Ok(_mapper.Map<ClientDTO>(client));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetClients([FromQuery] ClientsResourceParameters clientsResourceParameters)
        {
            try
            {
                var clients = await _clientsRepository.GetClientsAsync(clientsResourceParameters);
                if (clients != null)
                {
                    return Ok(_mapper.Map<IEnumerable<ClientDTO>>(clients));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return NotFound();
        }

        [HttpDelete("{clientId}")]
        public async Task<IActionResult> RemoveClient(int clientId)
        {
            try
            {
               var clientToRemove = await _clientsRepository.GetClientAsync(clientId);

               if(clientToRemove == null)
               {
                  return NotFound();
               }

               _dbRepository.Remove(clientToRemove);
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

        [HttpPut]
        public async Task<IActionResult> UpdateClientData(ClientDTO client)
        {
            try
            {
                if (client != null && await _clientsRepository.IsClientExistsAsync(client.Id))
                {
                    
                    if (await _clientsRepository.UpdateClientDataAsync(client))
                    {
                        if (await _dbRepository.SaveChangesAsync())
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

            return NotFound();
        }
    }
}
