using Data_Access_Layer.MoviesPortal;
using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using Manager.Helper;
using Manager.ModelsView;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class AccountManager : BaseManager, IAccountManager
    {
        public AccountManager(SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                              UsersMovieContext DbContext_User,
                              RoleManager<IdentityRole> _RoleManager,
                              NadjiUsera _NadjiUsera,
                              MoviesPortalContext _dbContext_Movies) : base(signInManager, userManager, DbContext_User, _RoleManager, _NadjiUsera, _dbContext_Movies)
        {

        }


        public async Task Login(Login_ModelView model)
        {
            ApplicationUser identityUser = _nadjiUsera.NadjiUseraPoUsername(model.Username);

            var result = await _signInManager.PasswordSignInAsync(identityUser, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded == false)
                throw new ValidationException("Neuspelo logovanje usera.");
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }



        public User_ModelView UsersALL()
        {
            User_ModelView model = new User_ModelView();
            foreach (ApplicationUser identityUser in _userManager.Users.ToList())
            {
                model.listaIdentityUsera.Add(_nadjiUsera.NadjiUseraPoUsername(identityUser.UserName));
            }
            return model;
        }
        public async Task KreirajUsera(User_ModelView model)
        {
            ApplicationUser identityUserNew = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Username
            };

            var result = await _userManager.CreateAsync(identityUserNew, model.Password);

            if (result.Succeeded)
            {
                User user = new User();
                user.Id = identityUserNew.Id;
                user.DateCreated = DateTime.Now;
                user.Active = model.Active;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Phone1 = model.Phone1;
                user.Phone2 = model.Phone2;
                _dbContext_Users.User.Add(user);

                UserCredential userCred = new UserCredential();
                userCred.Id = identityUserNew.Id;
                SHA512 alg = SHA512.Create();
                userCred.PasswordH = alg.ComputeHash(Encoding.ASCII.GetBytes(model.Password + "|AdGr++Tyul2021"));
                userCred.Username = model.Username;
                _dbContext_Users.UserCredentials.Add(userCred);

                UserAppPermission uap = new UserAppPermission();
                uap.Id = identityUserNew.Id;
                _dbContext_Users.UserAppPermission.Add(uap);

                _dbContext_Users.SaveChanges();
            }
            else
                throw new ValidationException("Nije uspelo kreiranje novog usera.");
        }
        public async Task<DetaljiUser_ModelView> DetaljiUser(string Id)
        {
            ApplicationUser identityUser = _nadjiUsera.NadjiUseraPoId(Id);

            DetaljiUser_ModelView model = new DetaljiUser_ModelView();

            StringBuilder sb = new StringBuilder();

            foreach (var role in _roleManager.Roles.ToList())
            {
                if (await _userManager.IsInRoleAsync(identityUser, role.Name) == false)
                {
                    model.Lista_nedodeljenih_Rola.Add(role);//puni listu rola koje user nema dodeljeno
                }
                else
                {
                    sb.Append(role.Name + " ");
                    model.Lista_dodeljenih_Rola.Add(role);//puni listu rola koje user ima dodeljeno
                }
            }

            model.DodeljeneRole = sb.ToString();
            model.Id = identityUser.Id;
            model.FirstName = identityUser.User.FirstName;
            model.LastName = identityUser.User.LastName;
            model.Email = identityUser.User.Email;
            model.Phone1 = identityUser.User.Phone1;
            model.Phone2 = identityUser.User.Phone2;
            model.Active = identityUser.User.Active;
            model.Username = identityUser.UserCredential.Username;

            return model;
        }
        public async Task IzmeniUsera(DetaljiUser_ModelView model)
        {
            ApplicationUser identityUserIncluded = _nadjiUsera.NadjiUseraPoId(model.Id);

            identityUserIncluded.UserName = model.Username;
            identityUserIncluded.Email = model.Email;
            var passhasher = new PasswordHasher<ApplicationUser>();
            var hashpass = passhasher.HashPassword(identityUserIncluded, model.Password);
            identityUserIncluded.PasswordHash = hashpass;

            identityUserIncluded.User.FirstName = model.FirstName;
            identityUserIncluded.User.LastName = model.LastName;
            identityUserIncluded.User.Email = model.Email;
            identityUserIncluded.User.Phone1 = model.Phone1;
            identityUserIncluded.User.Phone2 = model.Phone2;
            identityUserIncluded.User.Active = model.Active;

            identityUserIncluded.UserCredential.Username = model.Username;
            SHA512 alg = SHA512.Create();
            identityUserIncluded.UserCredential.PasswordH = alg.ComputeHash(Encoding.ASCII.GetBytes(model.Password + "|AdGr++Tyul2021"));

            var result = await _userManager.UpdateAsync(identityUserIncluded);


            if (result.Succeeded == false)
                throw new ValidationException("Neuspela izmena usera.");
        }
        public async Task ObrisiUsera(string Id)
        {
            ApplicationUser identityUser = _nadjiUsera.NadjiUseraPoId(Id);

            var result = await _userManager.DeleteAsync(identityUser);

            if (result.Succeeded == false)
                throw new ValidationException("Neuspelo brisanje usera.");
        }



        public async Task AccessDenied()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
