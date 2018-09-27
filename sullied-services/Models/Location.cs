using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string YelpId { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
    }
}
