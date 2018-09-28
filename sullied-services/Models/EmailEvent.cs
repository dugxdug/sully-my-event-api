using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models
{
    public class EmailEvent: Email
    {
        public DateTime EventStart { get; set; }
        public string Location { get; set; }
    }
}
