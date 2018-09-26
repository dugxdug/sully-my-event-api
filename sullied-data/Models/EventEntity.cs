using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sullied_data.Models
{
    [Table("Event")]
    public class EventEntity
    {
        protected EventEntity() { }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public int? LocationId { get; set; }
        [ForeignKey("User")]
        public int CreatedBy { get; set; }

        public virtual UserEntity User { get; set; }
        public ICollection<EventUserEntity> EventUsers { get; set; }
        public ICollection<EventLocationEntity> EventLocations { get; set; }
    }
}
