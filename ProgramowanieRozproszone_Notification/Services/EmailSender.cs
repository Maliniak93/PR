using ProgramowanieRozproszone_Notification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProgramowanieRozproszone_Notification.Services
{
    public class EmailSender
    {
        public void SendNewUserEmail(EmailMessage email)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("przetwarzanie.rozproszone.1993@gmail.com", "drzewo123"),
                EnableSsl = true,
            };

            smtpClient.Send("przetwarzanie.rozproszone.1993@gmail.com", email.EmailAddress, email.Subject, email.Body);
        }
    }
}
