using Manager.ModelsView;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Manager.Managers.MoviesManager
{
    public interface IMoviesManager
    {
        Movies_ModelView MoviesALL(ClaimsPrincipal LoggedUser);
        void Rent(int MovieId, ClaimsPrincipal user);
        void Unrent(int MovieId, ClaimsPrincipal u);
        DetaljiMovie_ModelView Detalji(int MovieId);
    }
}
