using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Account
{
    public class TambahVM
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Konfirmasi Password")]
        [Compare(nameof(Password), ErrorMessage = "{0} dan {1} harus sama"), ]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
