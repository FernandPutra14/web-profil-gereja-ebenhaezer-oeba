using PKMGerejaEbenhaezer.Web.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.KategoriIbadah
{
    public class EditVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Nama Kategori Ibadah")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public string Nama { get; set; } = string.Empty;

        [Display(Name = "Total Jam")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Range(0, 24, ErrorMessage = "{0} harus lebih dari {1} dan kurang dari sama dengan {2}")]
        public int TotalJam { get; set; }

        [Display(Name = "Total Menit")]
        [Required(ErrorMessage = "{0} harus diisi")]
        [Range(0, 59, ErrorMessage = "{0} harus lebih dari {1} dan kurang dari sama dengan {2}")]
        public int TotalMenit { get; set; }

        [MinTimeSpan(1)]
        public TimeSpan Durasi { get => new TimeSpan(TotalJam, TotalMenit, 0); }
    }
}
