using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sullied_data;
using sullied_services.Models;
using sullied_services.Services;

namespace sullied.Controllers
{
    [Route("v1/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly SulliedDbContext _db;

        public EmailController(SulliedDbContext context)
        {
            _db = context;
        }
        // PUT api/values/5
        [HttpPost]
        public void Email([FromBody] Email email)
        {
            var userEmails = _db.Users.Where(x => email.Recipients.Contains(x.Id)).Select(x => x.Email).ToList();
            try
            {
                new SmtpClient
                {
                    Host = "Smtp.Gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("sulliedhackathon@gmail.com", "SulliedHack")
                }
                .Send(new MailMessage
                {
                    From = new MailAddress(email.Sender, "The Sullier"),
                    To = { string.Join(",", userEmails )},
                    Subject = email.Subject,
                    Body = email.Body,
                    BodyEncoding = Encoding.UTF8
                });
            }
            catch (Exception ex)
            {
            }
        }
    }
}
