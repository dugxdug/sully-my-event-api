using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sullied_data.Models
{
    [Table("Location")]
    public class LocationEntity
    {
        protected LocationEntity() { }

        [Key]
        public int Id { get; set;  }
        public string YelpId { get; set; }
        public string Price { get; set; }
        public double Rating { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }

        public ICollection<EventLocationEntity> EventLocations { get; set; }
    }
}
