using PKMGerejaEbenhaezer.Domain.ValueObjects;
using PKMGerejaEbenhaezer.Web.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Rayon
{
    public class TambahVM : IRayonVM
    {
        [Display(Name = "Nama")]
        [Required(ErrorMessage = "{0} belum diisi")]
        public string Nama { get; set; } = string.Empty;

        [Display(Name = "Ketua Rayon")]
        [Required(ErrorMessage = "{0} belum diisi")]
        public string KetuaRayon { get; set; } = string.Empty;

        [Display(Name = "Nomor WA Ketua Rayon")]
        [RegularExpression(NoWa.ValidRegexPattern, ErrorMessage = "{0} tidak valid")]
        [StringLength(NoWa.ValidLength, ErrorMessage = "Panjang {0} harus {1}")]
        public string? NomorWa { get; set; }

        [Display(Name = "Foto Ketua")]
        [Required(ErrorMessage = "{0} harus diisi")]
        public int IdFoto { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} kurang dari {1} atau lebih dari {2}")]
        [Display(Name = "Jumlah Jemaat Laki - Laki")]
        [Required(ErrorMessage = "{0} belum diisi")]
        [TotalJemaat]
        public int JumlahLakiLaki { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} kurang dari {1} atau lebih dari {2}")]
        [Display(Name = "Jumlah Jemaat Perempuan")]
        [Required(ErrorMessage = "{0} belum diisi")]
        [TotalJemaat]
        public int JumlahPerempuan { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} kurang dari {1} atau lebih dari {2}")]
        [Display(Name = "Jumlah Jemaat Anak")]
        [Required(ErrorMessage = "{0} belum diisi")]
        [TotalJemaat]
        public int JumlahAnak { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} kurang dari {1} atau lebih dari {2}")]
        [Display(Name = "Jumlah Jemaat Remaja")]
        [Required(ErrorMessage = "{0} belum diisi")]
        [TotalJemaat]
        public int JumlahRemaja { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} kurang dari {1} atau lebih dari {2}")]
        [Display(Name = "Jumlah Jemaat Pemuda")]
        [Required(ErrorMessage = "{0} belum diisi")]
        [TotalJemaat]
        public int JumlahPemuda { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} kurang dari {1} atau lebih dari {2}")]
        [Display(Name = "Jumlah Jemaat Dewasa")]
        [Required(ErrorMessage = "{0} belum diisi")]
        [TotalJemaat]
        public int JumlahDewasa { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} kurang dari {1} atau lebih dari {2}")]
        [Display(Name = "Jumlah Jemaat Lansia")]
        [Required(ErrorMessage = "{0} belum diisi")]
        [TotalJemaat]
        public int JumlahLansia { get; set; }

        public int TotalBerdasarkanKelamin { get => JumlahLakiLaki + JumlahPerempuan; }
        public int TotalBerdasarkanUsia 
        { 
            get => JumlahAnak + JumlahRemaja + JumlahPemuda + JumlahDewasa + JumlahLansia;
        }
    }
}
