using PKMGerejaEbenhaezer.Web.CustomValidations;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.KategoriIbadah
{
    public class EditVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Nama Kategori Ibadah")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Nama { get; set; } = string.Empty;

        public Color Warna { get => Color.FromArgb(WarnaArgb); }

        [Display(Name = "Warna")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public int WarnaArgb { get; set; }

        [Display(Name = "Total Jam")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Range(0, 24, ErrorMessage = "{0} harus lebih dari {1} dan kurang dari sama dengan {2}")]
        public int TotalJam { get; set; }

        [Display(Name = "Total Menit")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Range(0, 59, ErrorMessage = "{0} harus lebih dari {1} dan kurang dari sama dengan {2}")]
        public int TotalMenit { get; set; }

        [MinTimeSpan(1, ErrorMessage = "{0} tidak boleh 0")]
        public TimeSpan Durasi { get => new(TotalJam, TotalMenit, 0); }
    }
}