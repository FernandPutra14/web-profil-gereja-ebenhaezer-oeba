using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.WartaJemaat
{
    public class EditVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Tanggal Warta")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public DateOnly TanggalWarta { get; set; }

        [Display(Name = "Link Dokumen Warta")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Url(ErrorMessage = "{0} bukan URL https atau http yang valid")]
        public string DocumentLink { get; set; } = string.Empty;

        [Display(Name = "Saldo Kas")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Range(0, 1e10, ErrorMessage = "{0} maksmimal {1} dan maksimal {2}")]
        [DataType(DataType.Currency, ErrorMessage = "{0} harus angka")]
        public double SaldoKas { get; set; }

        [Display(Name = "Penerimaan")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Range(0, 1e10, ErrorMessage = "{0} maksmimal {1} dan maksimal {2}")]
        [DataType(DataType.Currency, ErrorMessage = "{0} harus angka")]
        public double Penerimaan { get; set; }

        [Display(Name = "Pengeluaran")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Range(0, 1e10, ErrorMessage = "{0} maksmimal {1} dan maksimal {2}")]
        [DataType(DataType.Currency, ErrorMessage = "{0} harus angka")]
        public double Pengeluaran { get; set; }
    }
}
