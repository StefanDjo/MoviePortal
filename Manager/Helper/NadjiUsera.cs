using Data_Access_Layer.Users.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Manager.Helper
{
    public class NadjiUsera
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public NadjiUsera(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser NadjiUseraPoUsername(string UserName)
        {
            ApplicationUser identityUser = _userManager.Users.Where(x => x.UserName == UserName)
                              .Include(x => x.User)
                              .Include(x => x.UserAppPermission)
                              .Include(x => x.UserCredential)
                              .SingleOrDefault();

            if (identityUser != null)
                return identityUser;
            else
                throw new ValidationException("Nije nadjen User");
        }
        public ApplicationUser NadjiUseraPoId(string Id)
        {
            ApplicationUser identityUser = _userManager.Users.Where(x => x.Id == Id)
                              .Include(x => x.User)
                              .Include(x => x.UserAppPermission)
                              .Include(x => x.UserCredential)
                              .SingleOrDefault();

            if (identityUser != null)
                return identityUser;
            else
                throw new ValidationException("Nije nadjen User");
        }
    }
}
