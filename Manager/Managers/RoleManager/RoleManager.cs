using Data_Access_Layer.MoviesPortal;
using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using Manager.Helper;
using Manager.ModelsView;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Managers.RoleManager
{
    public class RoleManager : BaseManager, IRoleManager
    {
        public RoleManager(SignInManager<ApplicationUser> signInManager,
                      UserManager<ApplicationUser> userManager,
                      UsersMovieContext DbContext_User,
                      RoleManager<IdentityRole> _RoleManager,
                      NadjiUsera _NadjiUsera,
                      MoviesPortalContext _dbContext_Movies) : base(signInManager, userManager, DbContext_User, _RoleManager, _NadjiUsera, _dbContext_Movies)
        {

        }


        public Role_ModelView RoleALL()
        {
            Role_ModelView model = new Role_ModelView();
            model.ListaRola = _roleManager.Roles.ToList();
            return model;
        }
        public async Task KreirajRolu(Role_ModelView model)
        {
            IdentityRole ir = new IdentityRole()
            {
                Name = model.RoleNaziv
            };

            var result = await _roleManager.CreateAsync(ir);

            if (result.Succeeded == false)
                throw new ValidationException("Nije uspelo kreiranje nove role.");
        }
        public async Task<DetaljiRole_ModelView> DetaljiRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                DetaljiRole_ModelView model = new DetaljiRole_ModelView();
                model.RoleId = role.Id;
                model.RoleNaziv = role.Name;

                //Lista svih IdentityUsera na onosvu koje ja uzimam preko extended class UserId i nalazim User-e moje iz Proizvodnje etiketa
                foreach (var identityUser in _userManager.Users.ToList())
                {
                    //Proverava kome je sve dodeljena ova rola i puni listu usera kojima jeste i kojima nije dodeljena
                    if (await _userManager.IsInRoleAsync(identityUser, role.Name))
                    {
                        model.Useri_u_ovoj_Roli.Add(_nadjiUsera.NadjiUseraPoUsername(identityUser.UserName));//puni listu PE User-a kojima je dodeljena ova rola
                    }
                    else
                    {
                        model.Useri_koji_nisu_u_ovoj_Roli.Add(_nadjiUsera.NadjiUseraPoUsername(identityUser.UserName));//puni listu PE User-a kojima nije dodeljena ova rola
                    }
                }

                return model;
            }
            else
                throw new ValidationException("Nije nadjena Rola sa Id: " + Id);
        }
        public async Task DodeliRolu(string identityUserId, string roleId)
        {
            ApplicationUser identityUser = _nadjiUsera.NadjiUseraPoId(identityUserId);
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {
                if (await _userManager.IsInRoleAsync(identityUser, role.Name) == false)//Ispituje da li ovaj user vec ima ovu rolu, i samo ako nema, onda je dodaje
                {
                    var result = await _userManager.AddToRoleAsync(identityUser, role.Name);

                    if (result.Succeeded == false)
                        throw new ValidationException("Nije uspelo dodeljivanje Role sa Id:" + roleId + " useru sa Id: " + identityUserId);
                }
                else
                    throw new ValidationException("User sa Id:" + identityUserId + " vec ima dodeljenu ovu Rolu sa Id: " + roleId);
            }
            else
                throw new ValidationException("Nije nadjena rola sa RoleId: " + roleId);
        }
        public async Task ObrisiUseruRole(string identityUserId, string roleId)
        {
            ApplicationUser identityUser = _nadjiUsera.NadjiUseraPoId(identityUserId);
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);

            if (role != null)
            {
                if (await _userManager.IsInRoleAsync(identityUser, role.Name) == true)//Ispituje da li ovaj user vec ima ovu rolu, i samo ako je ima onda je brise
                {
                    var result = await _userManager.RemoveFromRoleAsync(identityUser, role.Name);

                    if (result.Succeeded == false)
                        throw new ValidationException("Nije uspelo brisanje Role sa Id:" + roleId + " useru sa Id: " + identityUserId);
                }
                else
                    throw new ValidationException("User sa Id:" + identityUserId + " nema dodeljenu Rolu sa Id: " + roleId);
            }
            else
                throw new ValidationException("Nije nadjena rola sa RoleId: " + roleId);
        }
        public async Task IzmeniRolu(DetaljiRole_ModelView model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role != null)
            {
                role.Name = model.RoleNaziv;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded == false)
                    throw new ValidationException("Nije uspela izmena role sa RoleId: " + model.RoleId);
            }
            else
                throw new ValidationException("Nije nadjena rola sa RoleId: " + model.RoleId);
        }
        public async Task ObrisiRolu(string Id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded == false)
                    throw new ValidationException("Nije uspelo brisanje role sa RoleId: " + Id);
              
            }
            else
                throw new ValidationException("Nije nadjena rola sa RoleId: " + Id);
        }
    }
}
