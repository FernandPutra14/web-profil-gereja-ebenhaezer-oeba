using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.WartaJemaat
{
    public class EditVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Tanggal Warta")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public DateTime TanggalWarta { get; set; }

        [Display(Name = "Link Dokumen Warta")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Url(ErrorMessage = "{0} bukan URL https atau http yang valid")]
        public string DocumentLink { get; set; } = string.Empty;
    }
}
