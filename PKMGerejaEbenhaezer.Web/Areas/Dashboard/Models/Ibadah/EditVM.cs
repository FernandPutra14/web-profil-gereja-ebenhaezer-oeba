using PKMGerejaEbenhaezer.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Ibadah
{
    public class EditVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Judul")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Judul { get; set; } = string.Empty;

        [Display(Name = "Deskripsi")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Deskripsi { get; set; } = string.Empty;

        [Display(Name = "Nats Pembimbing")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public AyatAlkitab NatsPembimbing { get; set; }

        [Display(Name = "Isi Nats Pembimbing")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string IsiNatspembimbing { get; set; } = string.Empty;

        [Display(Name = "Bacaan")]
        public AyatAlkitab? Bacaan { get; set; }

        [Display(Name = "Isi Bacaan")]
        public string? IsiBacaan { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Tanggal Ibadah")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public DateTime TanggalIbadah { get; set; }

        [Display(Name = "Tempat")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Tempat { get; set; } = string.Empty;

        [Display(Name = "Kategori Ibadah")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public int? IdKategoriIbadah { get; set; }

        [Display(Name = "Pendeta")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public int? IdPendeta { get; set; }
    }
}
