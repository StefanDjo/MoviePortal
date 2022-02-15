using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using Manager.Helper;
using Manager.Managers.RoleManager;
using Manager.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviePortal.Controllers
{
    public class RoleController : Controller
    {
        private IRoleManager _roleManager;

        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        public IActionResult Role()
        {
            return View(_roleManager.RoleALL());
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> KreirajRolu(Role_ModelView model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.KreirajRolu(model);
                return RedirectToAction("Role", "Role");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> DetaljiRole(string Id)
        {
            return View(await _roleManager.DetaljiRole(Id));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> DodeliUseruRolu(string identityUserId, string roleId, string RedirectController, string RedirectAction, string RedirectID)
        {
            await _roleManager.DodeliRolu(identityUserId, roleId);

            return RedirectToAction(RedirectAction, RedirectController, new { Id = RedirectID });
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ObrisiUseruRolu(string identityUserId, string roleId, string RedirectController, string RedirectAction, string RedirectID)
        {
            await _roleManager.ObrisiUseruRole(identityUserId, roleId);

            return RedirectToAction(RedirectAction, RedirectController, new { Id = RedirectID });
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> IzmeniRolu(DetaljiRole_ModelView model)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.IzmeniRolu(model);
                return RedirectToAction("DetaljiRole", "Role", new { Id = model.RoleId });
            }
            return RedirectToAction("DetaljiRole", "Role", new { Id = model.RoleId });
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ObrisiRolu(string Id)
        {
            await _roleManager.ObrisiRolu(Id);

            return RedirectToAction("Role", "Role");
        }

    }
}
