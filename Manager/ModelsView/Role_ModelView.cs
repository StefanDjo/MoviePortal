using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.ModelsView
{
    public class Role_ModelView
    {
        public Role_ModelView()
        {
            ListaRola = new List<IdentityRole>();
        }
        public List<IdentityRole> ListaRola { get; set; }


        [Key]
        [Required]
        public string RoleNaziv { get; set; }
    }
}
