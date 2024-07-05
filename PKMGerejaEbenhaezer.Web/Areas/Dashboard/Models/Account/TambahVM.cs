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
        [RegularExpression("/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$/", 
            ErrorMessage = "{0} harus minimal 8 karakter dan harus memiliki angka(1 - 9), huruf kecil(a - z) dan huruf besar(A - Z)")]
        [Required]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmasi Password")]
        [Compare(nameof(Password), ErrorMessage = "{0} harus sama dengan {1}")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
