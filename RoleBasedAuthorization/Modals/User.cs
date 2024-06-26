﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RoleBasedAuthorization.Modals
{
    public class User
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }


        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();


    }
}
