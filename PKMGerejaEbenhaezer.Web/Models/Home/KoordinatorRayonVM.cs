using PKMGerejaEbenhaezer.Domain.Entity;

namespace PKMGerejaEbenhaezer.Web.Models.Home
{
    public class KoordinatorRayonVM
    {
        public List<Rayon> DaftarRayon { get; set; } = new();

        public int TotalJemaat { get => TotalAnak + TotalRemaja + TotalPemuda + TotalDewasa + TotalLansia; }

        public int TotalAnak { get => DaftarRayon.Sum(r => r.JumlahAnak); }
        public int TotalRemaja { get => DaftarRayon.Sum(r => r.JumlahRemaja); }
        public int TotalPemuda { get => DaftarRayon.Sum(r => r.JumlahPemuda); }
        public int TotalDewasa { get => DaftarRayon.Sum(r => r.JumlahDewasa); }
        public int TotalLansia { get => DaftarRayon.Sum(r => r.JumlahLansia); }
    }
}
