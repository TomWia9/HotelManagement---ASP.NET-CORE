﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using HotelManagement.Data.DTO;
using HotelManagement.DTO;
using HotelManagement.Models;
using HotelManagement.ResourceParameters;
using HotelManagement.Services;
using HotelManagement.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<ActionResult<ClientDTO>> NewClient(ClientForCreationDTO client)
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

        [HttpPut("{clientId}")]
        public async Task<IActionResult> UpdateClient(int clientId, ClientForUpdateDTO client)
        {
            try
            {
                var clientFromRepo = await _clientsRepository.GetClientAsync(clientId);

                if(clientFromRepo == null)
                {
                    return NotFound();
                }

                _mapper.Map(client, clientFromRepo);
                _clientsRepository.UpdateClient(clientFromRepo);

                if(await _dbRepository.SaveChangesAsync())
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

        [HttpPatch("{clientId}")]
        public async Task<IActionResult> PartiallyUpdateClient(int clientId, JsonPatchDocument<ClientForUpdateDTO> patchDocument)
        {
            try
            {
                var clientFromRepo = await _clientsRepository.GetClientAsync(clientId);

                if (clientFromRepo == null)
                {
                    return NotFound();
                }

                var clientToPatch = _mapper.Map<ClientForUpdateDTO>(clientFromRepo);
                patchDocument.ApplyTo(clientToPatch, ModelState);
                _mapper.Map(clientToPatch, clientFromRepo);
                _clientsRepository.UpdateClient(clientFromRepo);

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
