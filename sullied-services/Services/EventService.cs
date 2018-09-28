using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using sullied_data;
using sullied_data.Models;
using sullied_services.Models;
using GoogleEvent = Google.Apis.Calendar.v3.Data.Event;

namespace sullied_services.Services
{
    public class EventService : IEventService
    {
        private readonly SulliedDbContext _db;

        public EventService(SulliedDbContext context)
        {
            _db = context;
        }
        public Models.Event GetEvent(int userId, int eventId)
        {
            return  _db.Events.Where(x => x.EventUsers.FirstOrDefault(eu => eu.UserId == userId) != null && x.Id == eventId).ProjectTo<Models.Event>().FirstOrDefault();
        }

        public List<Models.Event> GetEvents(int userId)
        {
            var events = _db.Events.Where(x => x.EventUsers.FirstOrDefault(eu => eu.UserId == userId) != null).ProjectTo<Models.Event>().ToList();
            foreach(var singleEvent in events)
            {
                var voted = singleEvent.EventUsers.FirstOrDefault(x => x.UserId == userId && x.LocationId != null);
                if (voted == null)
                {
                    singleEvent.HasVoted = false;
                } else
                {
                    singleEvent.HasVoted = true;
                }
            }
            foreach (var singleEvent in events)
            {
                var voteCounts = _db.EventUsers
               .Where(x => x.EventId == singleEvent.Id)
               .GroupBy(p => new { p.LocationId })
               .Select(g => new { id = g.Key.LocationId, count = g.Count() }).ToList();

                var locationNames = _db.Locations.Where(x => voteCounts.Select(vc => vc.id).Contains(x.Id)).ToList();

                var pollResults = new List<PollResults>();

                foreach (var location in locationNames)
                {
                    pollResults.Add(new PollResults() { LocationName = location.Name, NumberOfVotes = voteCounts.Where(x => x.id == location.Id).First().count });
                }

                singleEvent.PollResults = pollResults;
            }

            events = events.OrderBy(x => x.EventTime).ToList();

            return events;
        }

        public List<Models.Event> GetMyEvents(int userId)
        {
            var events = _db.Events.Where(x => x.User.Id == userId).ProjectTo<Models.Event>().ToList();
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

        public async Task<int> ClosePoll(int id)
        {
            var thisEvent = _db.Events.FirstOrDefault(x => x.Id == id);

            var voteCounts = _db.EventUsers
             .Where(x => x.EventId == id && x.LocationId != null)
             .GroupBy(p => new { p.LocationId })
             .Select(g => new { id = g.Key.LocationId, count = g.Count() })
             .ToList();
            
            var highestVote = voteCounts.OrderBy(x => x.count).FirstOrDefault();

            thisEvent.LocationId = highestVote.id;
            thisEvent.ImageUrl = _db.Locations.Where(x => x.Id == highestVote.id).Select(x => x.ImageUrl).FirstOrDefault();

            var userEmails = _db.Users.Where(x => x.EventUsers.Any(e => e.EventId == thisEvent.Id)).Select(x => x.Email).ToList();

            var attendees = new List<EventAttendee>();

            foreach (var userEmail in userEmails)
            {
                attendees.Add(new EventAttendee() { Email = userEmail });
            }
            _db.SaveChanges();
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "622377041033-1r91b6n360ic1f847kf1l3cdhliqg7r5.apps.googleusercontent.com",
                    ClientSecret = "dQRudG8v-JrDnTHBpM8rDoSN",
                },
                new[] { CalendarService.Scope.Calendar },
                "user",
                CancellationToken.None).Result;

            // Create the service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });

            GoogleEvent newEvent = new Google.Apis.Calendar.v3.Data.Event()
            {
                Summary = thisEvent.Title,
                Location = _db.Locations.FirstOrDefault(x => x.Id == thisEvent.LocationId).Name,
                Description = thisEvent.Description,
                Start = new EventDateTime()
                {
                    DateTime = thisEvent.Time,
                    TimeZone = "America/New_York"
                },
                End = new EventDateTime()
                {
                    DateTime = thisEvent.Time.AddHours(1),
                    TimeZone = "America/New_York",
                },
                Attendees = attendees,
                Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
                        new EventReminder() { Method = "email", Minutes = 24 * 60 }
                    }
                }
            };

            String calendarId = "primary";
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            request.SendNotifications = true;
            GoogleEvent createdEvent = request.Execute();
            Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);

            return _db.SaveChanges();
        }

        public int CreateEvent(Models.Event eventToCreate)
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

            return newEvent.Id;
        }

        public List<PollResults> GetPollResults(int id)
        {
            var voteCounts = _db.EventUsers
               .Where(x => x.EventId == id)
               .GroupBy(p => new { p.LocationId })
               .Select(g => new { id = g.Key.LocationId, count = g.Count() }).ToList();

            var locationNames = _db.Locations.Where(x => voteCounts.Select(vc => vc.id).Contains(x.Id)).ToList();

            var pollResults = new List<PollResults>();

            foreach(var location in locationNames)
            {
                pollResults.Add(new PollResults() { LocationName = location.Name, NumberOfVotes = voteCounts.Where(x => x.id == location.Id).First().count });
            }

            return pollResults;
        }
    }
}
