using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Users.Identity.UserClaim
{
    public class UserClaims : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        public UserClaims(UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager,
                          IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
        }



        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser identityUser)
        {
            identityUser = _userManager.Users.Where(x => x.UserName == identityUser.UserName)
                              .Include(x => x.User)
                              .Include(x => x.UserAppPermission)
                              .Include(x => x.UserCredential)
                              .SingleOrDefault();
            var principal = await base.CreateAsync(identityUser);


            if (!string.IsNullOrWhiteSpace(identityUser.User.Active.ToString()))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                 new Claim(ClaimTypes.Locality, identityUser.User.Active.ToString())
                });
            }


            return principal;
        }
    }
}
