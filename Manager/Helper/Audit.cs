using Data_Access_Layer.Audit;
using Data_Access_Layer.Users.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Helper
{
    public class Audit
    {
        private AuditContext _dbContext_audit;

        public Audit(AuditContext _DbContext_audit, UserManager<ApplicationUser> userManager)
        {
            _dbContext_audit = _DbContext_audit;
        }

        public void ZabeleziGresku(string poruka, string metoda, string IdentityUserID)
        {
            AuditGreske ag = new AuditGreske();
            ag.UserId = IdentityUserID;
            ag.Metoda = metoda;
            ag.Poruka = poruka;

            _dbContext_audit.AuditGreskes.Add(ag);
            _dbContext_audit.SaveChanges();
        }
    }
}
