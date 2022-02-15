using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.ModelsView
{
    public class DetaljiRole_ModelView
    {
        public DetaljiRole_ModelView()
        {
            Useri_u_ovoj_Roli = new List<ApplicationUser>();
            Useri_koji_nisu_u_ovoj_Roli = new List<ApplicationUser>();
        }
        public List<ApplicationUser> Useri_u_ovoj_Roli { get; set; }
        public List<ApplicationUser> Useri_koji_nisu_u_ovoj_Roli { get; set; }


        [Required]
        public string RoleId { get; set; }
        [Required]
        public string RoleNaziv { get; set; }
    }
}
