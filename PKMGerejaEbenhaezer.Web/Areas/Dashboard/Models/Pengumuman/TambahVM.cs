using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengumuman
{
    public class TambahVM
    {
        [Display(Name = "Judul Pengumuman")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Judul { get; set; }

        [Display(Name = "Isi Pengumuman")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Isi { get; set; }

        [Display(Name = "Foto Pengumuman")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public int IdFoto { get; set; }
    }
}
