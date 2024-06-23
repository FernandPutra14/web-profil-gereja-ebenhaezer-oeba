using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengumuman
{
    public class EditVM
    {
        [Display(Name = "Id Pengumuman")]
        [Required(ErrorMessage = "{0} belum diisi")]
        public int Id { get; set; }

        [Display(Name = "Judul Pengumuman")]
        [Required(ErrorMessage = "{0} belum diisi")]
        public string Judul { get; set; }

        [Display(Name = "Keterangan Pengumuman")]
        [Required(ErrorMessage = "{0} belum diisi")]
        public string Keterangan { get; set; }

        [Display(Name = "Tanggal Pengumuman")]
        [Required(ErrorMessage = "{0} belum diisi")]
        public DateTime Tanggal { get; set; }

        public string PathFoto { get; set; }

        [Display(Name = "Foto")]
        public IFormFile? FotoBaru { get; set; }
    }
}
