using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sullied_services.Models;
using sullied_services.Services;

namespace sullied.Controllers
{
    [Route("v1/events")]
    [ApiController]
    public class EventPollController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventPollController(IEventService eventService)
        {
            _eventService = eventService;
        }
        // PUT api/values/5
        [HttpPatch("{id}/close-poll")]
        public async void Put(int id)
        {
            await _eventService.ClosePoll(id);
        }
    }
}
