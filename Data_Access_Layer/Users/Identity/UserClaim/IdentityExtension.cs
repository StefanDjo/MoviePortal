using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Data_Access_Layer.Users.Identity.UserClaim
{
    public static class IdentityExtension
    {
        public static bool Active(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Locality);
            return (claim != null) ? Convert.ToBoolean(claim.Value) : false;
        }

    }
}
