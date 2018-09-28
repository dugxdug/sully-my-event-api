using sullied_services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace sullied_services.Services
{
    public interface IEventService
    {
        List<Event> GetEvents(int userId);
        List<PollResults> GetPollResults(int id);
        Event GetEvent(int userId, int eventId);
        int CreateEvent(Event eventToCreate);
        List<Event> GetMyEvents(int userId);
        List<Location> GetEventLocations(int userId, int Id);
        int AddVote(int userId, int id, EventUser value);
        int ClosePoll(int id);
    }
}
