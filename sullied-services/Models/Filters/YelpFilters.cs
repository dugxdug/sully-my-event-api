using System;
using System.Collections.Generic;
using System.Text;

namespace sullied_services.Models.Filters
{
    public class YelpFilters
    {
        public string Term { get; set; }
        public string Location { get; set; }
        public int Radius { get; set; }
        public string Categories { get; set; }
        public string Price { get; set; }
        public int OpenAt { get; set; }
    }
}
