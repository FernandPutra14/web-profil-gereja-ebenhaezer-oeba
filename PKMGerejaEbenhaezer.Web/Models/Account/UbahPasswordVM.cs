using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Models.Account
{
    public class UbahPasswordVM
    {
        [Display(Name = "Password Baru")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$",
            ErrorMessage = "{0} harus minimal 8 karakter dan harus memiliki angka(1 - 9), huruf kecil(a - z) dan huruf besar(A - Z)")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmasi Password")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Compare(nameof(Password), ErrorMessage = "{0} harus sama dengan {1}")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
