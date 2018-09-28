using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
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
                    To = { string.Join(",", userEmails) },
                    Subject = email.Subject,
                    Body = email.Body,
                    BodyEncoding = Encoding.UTF8
                });
            }
            catch (Exception ex)
            {
            }
        }

        [HttpPut()]
        public void SendCalenderEvent([FromBody] EmailEvent email)
        {
            CalendarSerializer serializer = new CalendarSerializer(new SerializationContext());
            var userEmails = _db.Users.Where(x => email.Recipients.Contains(x.Id)).Select(x => new { userEmail = x.Email, displayName = string.Format("{0} {1}", x.FirstName, x.LastName) }).ToList();
            var client = new SmtpClient
            {
                Host = "Smtp.Gmail.com",
                Port = 587,
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("sulliedhackathon@gmail.com", "SulliedHack")
            };
            var newEvent = new CalendarEvent
            {
                DtStart = new CalDateTime(DateTime.Now.AddDays(2)),
                DtEnd = new CalDateTime(DateTime.Now.AddDays(2).AddHours(1)),
                DtStamp = new CalDateTime(DateTime.Now),
                Sequence = 0,
                Transparency = TransparencyType.Transparent,
                Description = "Test with iCal.Net",
                Priority = 5,
                Class = "PUBLIC",
                Location = "New York",
                Summary = "Tested with iCal.Net Summary",
                Uid = Guid.NewGuid().ToString(),
                Organizer = new Organizer()
                {
                    CommonName = "John, Song",
                    Value = new Uri(string.Format("mailto:{0}", email.Sender))
                }
            };

            foreach (var userEmail in userEmails)
            {
                try
                {
                    newEvent.Attendees.Add(new Attendee()
                    {
                        CommonName = userEmail.displayName,
                        ParticipationStatus = "REQ-PARTICIPANT",
                        Rsvp = true,
                        Value = new Uri(string.Format("mailto:{0}", userEmail.userEmail))
                    });

                    Calendar c = new Calendar();
                    c.Method = "REQUEST";
                    c.Events.Add(newEvent);
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
                    ct.Parameters.Add("method", "REQUEST");
                    AlternateView avCal = AlternateView.CreateAlternateViewFromString(serializer.SerializeToString(c), ct);
                    
                    MailMessage msg = new MailMessage
                    {
                        From = new MailAddress(email.Sender, "The Sullier"),
                        To = { new MailAddress(userEmail.userEmail, userEmail.displayName) },
                        Subject = email.Subject,
                        Body = email.Body,
                        BodyEncoding = Encoding.UTF8


                    };

                    msg.AlternateViews.Add(avCal);
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
