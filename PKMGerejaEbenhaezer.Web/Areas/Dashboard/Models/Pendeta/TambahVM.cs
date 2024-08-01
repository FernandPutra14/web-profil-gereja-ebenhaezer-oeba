using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pendeta
{
    public class TambahVM
    {
        [Display(Name = "Nama Pendeta")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Nama { get; set; } = string.Empty;

        [Display(Name = "Foto Pendeta")]
        [Required(ErrorMessage = "{0} harus dipilih")]
        public int IdFoto { get; set; }

        [Display(Name = "Jabatan Pendeta")]
        //[Required(ErrorMessage = "{0} harus diisi")]
        public string? jabatan { get; set; }

        [Url(ErrorMessage = "{0} bukan URL https atau http yang valid")]
        [Display(Name = "Profil Facebook")]
        public string? FacebookProfileLink { get; set; }

        [Url(ErrorMessage = "{0} bukan URL https atau http yang valid")]
        [Display(Name = "Profil Instagram")]
        public string? InstagramProfileLink { get; set; }

        [Url(ErrorMessage = "{0} bukan URL https atau http yang valid")]
        [Display(Name = "Profil Youtube")]
        public string? YoutubeProfileLink { get; set; }
    }
}
