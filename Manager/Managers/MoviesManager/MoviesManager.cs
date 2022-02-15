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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Managers.MoviesManager
{
    public class MoviesManager : BaseManager, IMoviesManager
    {
        public MoviesManager(SignInManager<ApplicationUser> signInManager,
              UserManager<ApplicationUser> userManager,
              UsersMovieContext DbContext_User,
              RoleManager<IdentityRole> _RoleManager,
              NadjiUsera _NadjiUsera,
              MoviesPortalContext _dbContext_Movies) : base(signInManager, userManager, DbContext_User, _RoleManager, _NadjiUsera, _dbContext_Movies)
        {

        }

        public Movies_ModelView MoviesALL(ClaimsPrincipal LoggedUser)
        {
            Movies_ModelView model = new Movies_ModelView();

            if (_signInManager.IsSignedIn(LoggedUser) == true)
            {
                ApplicationUser user = _nadjiUsera.NadjiUseraPoUsername(LoggedUser.Identity.Name);
                model.listaMovieUnrent = _dbContext_Movies.Movies.Where(x => x.Available == false && x.UserId == user.Id).ToList();
                model.listaMovieRent = _dbContext_Movies.Movies.Where(x => x.Available == true).ToList();
            }
            else
            {
                model.listaMovieRent = _dbContext_Movies.Movies.Where(x => x.Available == true).ToList();
            }

            return model;
        }
        public void Rent(int MovieId, ClaimsPrincipal u)
        {
            ApplicationUser user = _nadjiUsera.NadjiUseraPoUsername(u.Identity.Name);
            Movie m = _dbContext_Movies.Movies.Where(x => x.MovieId == MovieId).FirstOrDefault();
            if (m != null)
            {
                if (m.Available == true)
                {
                    m.Available = false;
                    m.UserId = user.Id;

                    _dbContext_Movies.Movies.Update(m);
                    _dbContext_Movies.SaveChanges();
                }
                else
                    throw new ValidationException("Film vise nije dostupan za rent.");
            }
            else
                throw new ValidationException("Nije pronadjen film sa Id: " + MovieId.ToString());
        }
        public void Unrent(int MovieId, ClaimsPrincipal LoggedUser)
        {
            ApplicationUser user = _nadjiUsera.NadjiUseraPoUsername(LoggedUser.Identity.Name);
            Movie m = _dbContext_Movies.Movies.Where(x => x.MovieId == MovieId).FirstOrDefault();
            if (m != null)
            {
                if (m.Available == false)
                {
                    m.Available = true;
                    m.UserId = null;

                    _dbContext_Movies.Movies.Update(m);
                    _dbContext_Movies.SaveChanges();
                }
                else
                    throw new ValidationException("Film je Unrent-ovan.");
            }
            else
                throw new ValidationException("Nije pronadjen film sa Id: " + MovieId.ToString());
        }
        public DetaljiMovie_ModelView Detalji(int MovieId)
        {
            Movie m = _dbContext_Movies.Movies.Where(x => x.MovieId == MovieId).FirstOrDefault();

            if (m != null)
            {
                DetaljiMovie_ModelView model = new DetaljiMovie_ModelView();
                model.MovieId = m.MovieId;
                model.Author = m.Author;
                model.Available = m.Available;
                model.Duration = m.Duration;
                model.Title = m.Title;
                model.UserId = m.UserId;

                return model;
            }
            else
                throw new ValidationException("Nije nadjen Movie sa Id:" + MovieId.ToString());
        }
    }
}
