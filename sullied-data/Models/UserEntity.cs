﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sullied_data.Models
{
    [Table("User")]
    public class UserEntity
    {
        public UserEntity() { }

        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public ICollection<EventUserEntity> EventUsers { get; set; }
    }
}
