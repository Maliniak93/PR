using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramowanieRozproszone_Notification.Model;
using ProgramowanieRozproszone_Notification.Services;

namespace ProgramowanieRozproszone_Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        public void SendMessage(EmailMessage request)
        {
            EmailSender sender = new EmailSender();

            sender.SendNewUserEmail(request);
        }
    }
}
