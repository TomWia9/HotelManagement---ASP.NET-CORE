using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Dto;
using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly HotelManagementContext _context;
        private readonly ClientsService _clientsService;
        private readonly IMapper _mapper;

        public ClientsController(HotelManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _clientsService = new ClientsService(context, mapper);
        }

        [HttpPost("NewClient")]
        public async Task<ActionResult<Client>> NewClient(ClientDto client)
        {
            try
            {
                var newClient = _mapper.Map<Client>(client);

                _clientsService.Add(newClient);
                if (await _clientsService.SaveChangesAsync())
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

        [HttpGet("GetClient/{clientId}")]
        public async Task<ActionResult<ClientDto>> GetClient(int clientId)
        {
            try
            {
                var client = await _clientsService.GetClientAsync(clientId);
                if (client != null)
                {
                    return Ok(_mapper.Map<ClientDto>(client));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("RemoveClient/{clientId}")]
        public async Task<IActionResult> RemoveClient(int clientId)
        {
            try
            {
                if (await _clientsService.ClientExists(clientId))
                {
                    var clientToRemove = await _clientsService.GetClientAsync(clientId);
                    _clientsService.Remove(clientToRemove);
                    if (await _clientsService.SaveChangesAsync())
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

        [HttpPut("UpdateClientData")]
        public async Task<IActionResult> UpdateClientData(ClientDto client)
        {
            try
            {
                if (client != null && await _clientsService.ClientExists(client.Id))
                {
                    
                    if (await _clientsService.UpdateClientData(client))
                    {
                        if (await _clientsService.SaveChangesAsync())
                        {
                            return Ok();
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
