using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managers
{
    public class BaseManager
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected UsersContext _dbContext_Users;

        public BaseManager(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, UsersContext DbContext_User, RoleManager<IdentityRole> _RoleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = _RoleManager;
            _dbContext_Users = DbContext_User;
        }
    }
}
