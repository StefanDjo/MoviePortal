using Data_Access_Layer.Users.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Layer.Users
{
    public partial class UserCredential
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordH { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
