using PKMGerejaEbenhaezer.Web.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengaturan;

public class FotoVM
{
    [Display(Name = "Minimal Ukuran Upload (kB)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(minimum: 10, maximum: 10 * 1024, ErrorMessage = "Harus di antara 10kB - 10MB")]
    [LessThan(nameof(MaxSizeLimit))]
    public long MinSizeLimit { get; set; }

    [Display(Name = "Maksimal Ukuran Upload (kB)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(minimum: 10, maximum: 10 * 1024, ErrorMessage = "Harus di antara 10kB - 10MB")]
    public long MaxSizeLimit { get; set; }

    [Display(Name = "Kualitas Kompresi (0 - 100)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(0, 100, ErrorMessage = "Harus antara {0} dan {1}")]
    public int CompressionQuality { get; set; }

    [Display(Name = "Ukuran Foto Small (Tinggi)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(1, 10000, ErrorMessage = "Harus antara {0} dan {1}")]
    public int SmallHeight { get; set; }

    [Display(Name = "Ukuran Foto Small (Lebar)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(1, 10000, ErrorMessage = "Harus antara {0} dan {1}")]
    public int SmallWidth { get; set; }

    [Display(Name = "Ukuran Foto Medium (Tinggi)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(1, 10000, ErrorMessage = "Harus antara {0} dan {1}")]
    public int MediumHeight { get; set; }

    [Display(Name = "Ukuran Foto Medium (Lebar)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(1, 10000, ErrorMessage = "Harus antara {0} dan {1}")]
    public int MediumWidth { get; set; }

    [Display(Name = "Ukuran Foto Large (Tinggi)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(1, 10000, ErrorMessage = "Harus antara {0} dan {1}")]
    public int LargeHeight { get; set; }

    [Display(Name = "Ukuran Foto Large (Lebar)")]
    [Required(ErrorMessage = "Harus Diisi")]
    [Range(1, 10000, ErrorMessage = "Harus antara {0} dan {1}")]
    public int LargeWidth { get; set; }
}
