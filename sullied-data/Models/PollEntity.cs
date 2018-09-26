using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sullied_data.Models
{
    [Table("Poll")]
    public class PollEntity
    {
        protected PollEntity() { }

        [Key]
        public int Id { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public DateTime Expiration { get; set; }
        public bool Closed { get; set; }
    }
}
