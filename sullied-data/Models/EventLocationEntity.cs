using System.ComponentModel.DataAnnotations.Schema;

namespace sullied_data.Models
{
    [Table("EventLocation")]
    public class EventLocationEntity
    {
        public EventLocationEntity() { }

        public int EventId { get; set; }
        public EventEntity Event { get; set; }
        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
    }
}
