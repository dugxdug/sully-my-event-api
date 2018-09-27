using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using sullied_data;
using sullied_data.Models;
using sullied_services.Models;

namespace sullied_services.Services
{
    public class EventService : IEventService
    {
        private readonly SulliedDbContext _db;

        public EventService(SulliedDbContext context)
        {
            _db = context;
        }
        public Event GetEvent(int userId, int eventId)
        {
            return  _db.Events.Where(x => x.EventUsers.Any(eu => eu.UserId == userId) && x.Id == eventId).ProjectTo<Event>().FirstOrDefault();
        }

        public List<Event> GetEvents(int userId)
        {
            var events = _db.Events.Where(x => x.EventUsers.Where(eu => eu.UserId == userId).FirstOrDefault() != null).ProjectTo<Event>().ToList();

            return events;
        }

        public List<Event> GetMyEvents(int userId)
        {
            var events = _db.Events.Where(x => x.User.Id == userId).ProjectTo<Event>().ToList();

            return events;
        }

        public List<Location> GetEventLocations(int userId, int id)
        {
            var eventLocations = _db.Locations.Where(x => x.EventLocations.Any(el => el.EventId == id)).ProjectTo<Location>().ToList();


            return eventLocations;
        }

        public int CreateEvent(Event eventToCreate)
        {
            var locations = new List<EventLocationEntity>();
            foreach (var location in eventToCreate.EventLocations)
            {
                locations.Add(new EventLocationEntity { LocationId = location.LocationId });
            }

            var users = new List<EventUserEntity>();
            foreach(var user in eventToCreate.EventUsers)
            {
                users.Add(new EventUserEntity { UserId = user.UserId });
            }

            var newEvent = new EventEntity
            {
                Title = eventToCreate.Title,
                Description = eventToCreate.Description,
                Time = eventToCreate.EventTime,
                CreatedBy = eventToCreate.CreatedById,
                EventLocations = locations,
                EventUsers = users
            };


            var createdEvent = _db.Events.Add(newEvent);

            var result = _db.SaveChanges();

            return result;
        }
    }
}
