using Data_Access_Layer.Users.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Data_Access_Layer.Users
{
    public partial class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
