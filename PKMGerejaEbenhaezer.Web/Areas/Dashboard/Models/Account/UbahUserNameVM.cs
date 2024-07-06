using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Account
{
    public class UbahUserNameVM
    {
        [Display(Name = "User Name Baru")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string UserName { get; set; } = string.Empty;
    }
}
