using Manager.ModelsView;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public interface IAccountManager
    {
        Task Login(Login_ModelView model);
        Task Logout();
        User_ModelView UsersALL();
        Task KreirajUsera(User_ModelView model);
        Task<DetaljiUser_ModelView> DetaljiUser(string Id);
        Task IzmeniUsera(DetaljiUser_ModelView model);
        Task ObrisiUsera(string Id);
        Task AccessDenied();
    }
}
