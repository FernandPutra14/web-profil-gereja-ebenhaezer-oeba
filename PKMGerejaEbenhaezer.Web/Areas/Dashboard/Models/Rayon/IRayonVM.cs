namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Rayon
{
    public interface IRayonVM
    {
        int JumlahLakiLaki { get; set; }

        int JumlahPerempuan { get; set; }

        int JumlahAnak { get; set; }

        int JumlahRemaja { get; set; }

        int JumlahPemuda { get; set; }

        int JumlahDewasa { get; set; }

        int JumlahLansia { get; set; }

        int TotalBerdasarkanKelamin { get => JumlahLakiLaki + JumlahPerempuan; }
        int TotalBerdasarkanUsia
        {
            get => JumlahAnak + JumlahRemaja + JumlahPemuda + JumlahDewasa + JumlahLansia;
        }
    }
}
