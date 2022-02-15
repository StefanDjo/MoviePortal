using Data_Access_Layer.Users;
using Data_Access_Layer.Users.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.ModelsView
{
    public class User_ModelView
    {
        public User_ModelView()
        {
            listaIdentityUsera = new List<ApplicationUser>();
        }
        public List<ApplicationUser> listaIdentityUsera { get; set; }


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
        public DateTime DateCreated { get; set; }



        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Comfirm password")]
        [Compare("Password", ErrorMessage = "Password and comfirmation password do not match.")]
        public string ComfirmPassword { get; set; }
    }
}
