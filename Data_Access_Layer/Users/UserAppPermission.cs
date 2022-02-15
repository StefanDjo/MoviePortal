using Data_Access_Layer.Users.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Layer.Users
{
    public partial class UserAppPermission
    {
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
