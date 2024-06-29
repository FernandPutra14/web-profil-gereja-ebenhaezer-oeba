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

        [Display(Name = "Isi Pengumuman")]
        [Required(ErrorMessage = "{0} belum diisi")]
        public string Isi { get; set; }

        [Display(Name = "Foto Pengumuman")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public int? IdFoto { get; set; }
    }
}
