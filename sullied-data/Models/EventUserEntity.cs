using System.ComponentModel.DataAnnotations.Schema;

namespace sullied_data.Models
{
    [Table("EventUser")]
    public class EventUserEntity
    {
        protected EventUserEntity() { }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int EventId { get; set; }
        public EventEntity Event { get; set; }
        public int? LocationId { get; set; }
    }
}
