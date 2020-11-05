using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR.Modul1.Services;
using ProgramowanieRozproszone_1.Model;

namespace ProgramowanieRozproszone_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ServiceBusSender _sender;

        public PatientsController(DataContext context, ServiceBusSender sender)
        {
            _context = context;
            _sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Patients.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Patients.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Patient p)
        {
            _context.Patients.Add(p);
            await _context.SaveChangesAsync();
            await _sender.SendMessage(new MessagePayload()
            {
                EventName = "NewUserRegistered",
                EmailAddress = p.Email,
                Subject = "Covid19",
                Body = "idziesz na kwarantanne"
            });
            return Created(uri: "/api/users/" + p.Id, p);
        }
    }
}
