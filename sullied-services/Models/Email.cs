using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models
{
    public class Email
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<int> Recipients { get; set; }
    }
}
