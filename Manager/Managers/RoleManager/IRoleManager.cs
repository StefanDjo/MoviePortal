using Manager.ModelsView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Managers.RoleManager
{
    public interface IRoleManager
    {
        Role_ModelView RoleALL();
        Task KreirajRolu(Role_ModelView model);
        Task<DetaljiRole_ModelView> DetaljiRole(string Id);
        Task DodeliRolu(string identityUserId, string roleId);
        Task ObrisiUseruRole(string identityUserId, string roleId);
        Task IzmeniRolu(DetaljiRole_ModelView model);
        Task ObrisiRolu(string Id);
    }
}
