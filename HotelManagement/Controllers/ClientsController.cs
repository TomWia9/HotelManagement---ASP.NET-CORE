using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.Dto;
using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly HotelManagementContext _context;
        private readonly ClientsService clientsService;
        private readonly IMapper _mapper;

        public ClientsController(HotelManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            clientsService = new ClientsService(context);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> NewClient(ClientDto client)
        {
            try
            {
                var newClient = _mapper.Map<Client>(client);
                newClient.AddressId = await clientsService.CreateAddress(client.Address);

                clientsService.Add(newClient);
                if (await clientsService.SaveChangesAsync())
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
    }
}
