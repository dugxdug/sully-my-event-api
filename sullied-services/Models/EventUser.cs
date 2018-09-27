using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models
{
    public class EventUser
    {
        public int UserId { get; set; }
        public int? LocationId { get; set; }
        public int EventId { get; set; }
    }
}
