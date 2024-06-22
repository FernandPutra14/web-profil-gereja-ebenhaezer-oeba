using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Models.Account
{
    public class LoginVM
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "{0} tidak diisi")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} tidak diisi")]
        public string Password { get; set; }

        [Display(Name = "Ingat Saya")]
        [Required]
        public bool RememberMe { get; set; } = false;
    }
}
