using Manager.Managers.MoviesManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MoviePortal.Controllers
{
    public class HomeController : Controller
    {
        private IMoviesManager _movieManager;

        public HomeController(IMoviesManager _MovieManager)
        {
            _movieManager = _MovieManager;
        }

        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_movieManager.MoviesALL(User));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Korisnik")]
        public IActionResult Rent(int MovieId)
        {
            _movieManager.Rent(MovieId, User);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Korisnik")]
        public IActionResult Unrent(int MovieId)
        {
            _movieManager.Unrent(MovieId, User);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Detalji(int MovieId)
        {
            return View(_movieManager.Detalji(MovieId));
        }
    }
}
