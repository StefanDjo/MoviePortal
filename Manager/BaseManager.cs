using Data_Access_Layer.MoviesPortal;
using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using Manager.Helper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager
{
    public class BaseManager
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected UsersMovieContext _dbContext_Users;
        protected MoviesPortalContext _dbContext_Movies;
        protected NadjiUsera _nadjiUsera;

        public BaseManager(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, UsersMovieContext DbContext_User, RoleManager<IdentityRole> _RoleManager, NadjiUsera _NadjiUsera, MoviesPortalContext _DbContext_Movies)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = _RoleManager;
            _dbContext_Users = DbContext_User;
            _nadjiUsera = _NadjiUsera;
            _dbContext_Movies = _DbContext_Movies;
        }
    }
}
