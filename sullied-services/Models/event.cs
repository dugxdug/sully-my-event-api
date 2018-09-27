using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventTime { get; set; }
        public string LocationId { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public List<Location> SelectedLocations { get; set; }
        public List<EventLocation> EventLocations { get; set; }
        public List<EventUser> EventUsers { get; set; }
    }
}
