using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ClientsController(HotelManagementContext context)
        {
            _context = context;
            clientsService = new ClientsService(context);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> NewClient(ClientDto client)
        {
            try
            {
                //have to add automapper
                var newClient = new Client()
                {
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Sex = client.Sex,
                    Age = client.Age,
                    AddressId = await clientsService.CreateAddress(client.Address),
                };
                
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
