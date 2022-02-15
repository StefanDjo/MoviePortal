using Data_Access_Layer.Users.Identity;
using Manager.Helper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviePortal.Controllers
{
    public class ErrorController : Controller
    {
        private NadjiUsera _nadjiUsera;
        private Audit _audit;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        public ErrorController(NadjiUsera nadjiUsera, Audit audit, SignInManager<ApplicationUser> signInManager)
        {
            _nadjiUsera = nadjiUsera;
            _audit = audit;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> ErrorPage()
        {
            var result = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _audit.ZabeleziGresku(result.Error.Message, result.Path, _nadjiUsera.NadjiUseraPoUsername(User.Identity.Name).Id);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { errorPoruka = result.Error.Message });
        }
    }
}
