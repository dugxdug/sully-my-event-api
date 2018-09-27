using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sullied_services.Models;
using sullied_services.Services;

namespace sullied.Controllers
{
    [Route("v1/users/{userId}/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Event> Get(int userId)
        {
            return _eventService.GetEvents(userId);
        }

        [HttpGet("{id}/me")]
        public IEnumerable<Event> GetMyEvents(int userId)
        {
            return _eventService.GetMyEvents(userId);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Event Get(int userId, int id)
        {
            return _eventService.GetEvent(userId,id);
        }

        [HttpGet("{id}/locations")]
        public IEnumerable<Location> GetLocations(int userId, int id)
        {
            return _eventService.GetEventLocations(userId, id);
        }
        // POST api/values
        [HttpPost]
        public int Post([FromBody] Event eventModel)
        {
            return _eventService.CreateEvent(eventModel);
        }

        // PUT api/values/5
        [HttpPut("{id}/vote")]
        public void Put(int userId, int id, [FromBody] EventUser value)
        {
            var result = _eventService.AddVote(userId, id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
