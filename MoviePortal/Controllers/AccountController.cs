using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using Manager;
using Manager.Helper;
using Manager.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MoviePortal.Controllers
{
    public class AccountController : Controller
    {
        private IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        //[AllowAnonymous]
        public IActionResult Users()
        {
            return View(_accountManager.UsersALL());
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> KreirajUser(User_ModelView model)
        {
            if (ModelState.IsValid)
            {
                await _accountManager.KreirajUsera(model);
                return RedirectToAction("Users", "Account");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        //[AllowAnonymous]
        public async Task<IActionResult> DetaljiUser(string Id)
        {
            return View(await _accountManager.DetaljiUser(Id));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        //[AllowAnonymous]
        public async Task<IActionResult> IzmeniUser(DetaljiUser_ModelView model)
        {
            if (ModelState.IsValid)
            {
                await _accountManager.IzmeniUsera(model);
                return RedirectToAction("DetaljiUser", "Account", new { Id = model.Id });
            }
            return RedirectToAction("DetaljiUser", "Account", new { Id = model.Id });
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ObrisiUsera(string Id)
        {
            await _accountManager.ObrisiUsera(Id);
            return RedirectToAction("Users", "Account");
        }




        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string errorPoruka = null)
        {
            ViewData["errorPoruka"] = errorPoruka;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login_ModelView model)
        {
            if (ModelState.IsValid)
            {
                await _accountManager.Login(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountManager.Logout();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied()
        {
            await _accountManager.AccessDenied();
            return RedirectToAction("Login", "Account", new { errorPoruka = "Nemate pristup!" });
        }
    }
}
