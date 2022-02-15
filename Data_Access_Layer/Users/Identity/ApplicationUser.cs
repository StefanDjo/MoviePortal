using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Users.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual User User { get; set; }
        public virtual UserCredential UserCredential { get; set; }
        public virtual UserAppPermission UserAppPermission { get; set; }
    }
}
