using PKMGerejaEbenhaezer.Domain.Entity;

namespace PKMGerejaEbenhaezer.Web.Models.Home
{
    public class IndexVM
    {
        public List<Pengumuman> DaftarPengumuman { get; set; } = new();
        public List<WartaJemaat> DaftarWartaJemaat { get; set; } = new();
        public List<Pendeta> DaftarPendeta { get; set; } = new();
        public List<Ibadah> DaftarIbadah { get; set; } = new();

        public int TotalJemaat { get => TotalAnak + TotalRemaja + TotalPemuda + TotalDewasa + TotalLansia; }

        public int TotalAnak { get; set; }
        public int TotalRemaja { get; set; }
        public int TotalPemuda { get; set; }
        public int TotalDewasa { get; set; }
        public int TotalLansia { get; set; }
    }
}
