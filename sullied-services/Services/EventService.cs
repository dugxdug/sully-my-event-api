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
            return  _db.Events.Where(x => x.EventUsers.FirstOrDefault(eu => eu.UserId == userId) != null && x.Id == eventId).ProjectTo<Event>().FirstOrDefault();
        }

        public List<Event> GetEvents(int userId)
        {
            var events = _db.Events.Where(x => x.EventUsers.FirstOrDefault(eu => eu.UserId == userId) != null).ProjectTo<Event>().ToList();

            return events;
        }

        public List<Event> GetMyEvents(int userId)
        {
            var events = _db.Events.Where(x => x.User.Id == userId).ProjectTo<Event>().ToList();

            return events;
        }

        public List<Location> GetEventLocations(int userId, int id)
        {
            var eventLocations = _db.Locations
                .Where(
                    x => x.EventLocations
                        .Where(el => el.EventId == id && el.Event.EventUsers.FirstOrDefault(eu => eu.UserId == userId && eu.LocationId == null) != null)
                        .FirstOrDefault() != null).ProjectTo<Location>().ToList();

            return eventLocations;
        }

        public int AddVote(int userId, int id, EventUser value)
        {
            var vote = _db.EventUsers.FirstOrDefault(x => x.UserId == userId && x.EventId == id);

            vote.LocationId = value.LocationId;

            return _db.SaveChanges();      
        }

        public int CreateEvent(Event eventToCreate)
        {
            var newLocations = new List<LocationEntity>();
            foreach(var locationToAdd in eventToCreate.SelectedLocations)
            {
                if(_db.Locations.Any(x => x.YelpId == locationToAdd.YelpId))
                {
                    continue;
                }
                _db.Locations.Add(new LocationEntity
                {
                    YelpId = locationToAdd.YelpId,
                    Price = locationToAdd.Price,
                    Rating = locationToAdd.Rating,
                    Name = locationToAdd.Name,
                    Address = locationToAdd.Address,
                    ImageUrl = locationToAdd.ImageUrl,
                    Url = locationToAdd.Url
                });
            }

            _db.SaveChanges();

            var locations = new List<EventLocationEntity>();
            foreach (var locationToAdd in eventToCreate.SelectedLocations)
            {
                var location = _db.Locations.FirstOrDefault(x => x.YelpId == locationToAdd.YelpId);
                locations.Add(new EventLocationEntity { LocationId = location.Id });
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
