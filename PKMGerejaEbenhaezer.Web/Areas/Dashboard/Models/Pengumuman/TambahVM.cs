using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengumuman
{
    public class TambahVM
    {
        [Display(Name = "Judul Pengumuman")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Judul { get; set; }

        [Display(Name = "Keterangan Pengumuman")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Keterangan { get; set; }

        [Display(Name = "Tanggal Pengumuman")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public DateTime Tanggal { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public IFormFile Foto { get; set; }
    }
}
