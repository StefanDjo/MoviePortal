using Data_Access_Layer.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.ModelsView
{
    public class DetaljiUser_ModelView
    {
        public DetaljiUser_ModelView()
        {
            Lista_dodeljenih_Rola = new List<IdentityRole>();
            Lista_nedodeljenih_Rola = new List<IdentityRole>();
        }
        public List<IdentityRole> Lista_dodeljenih_Rola { get; set; }
        public List<IdentityRole> Lista_nedodeljenih_Rola { get; set; }


        [Required]
        public string Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "e-mail")]
        public string Email { get; set; }
        [Display(Name = "Phone - first")]
        public string Phone1 { get; set; }
        [Display(Name = "Phone - second")]
        public string Phone2 { get; set; }
        [Required]
        [Display(Name = "Active user")]
        public bool Active { get; set; }


        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string DodeljeneRole { get; set; }
    }
}
