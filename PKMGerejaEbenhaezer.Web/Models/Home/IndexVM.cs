using PKMGerejaEbenhaezer.Domain;

namespace PKMGerejaEbenhaezer.Web.Models.Home
{
    public class IndexVM
    {
        public List<Pengumuman>? DaftarPengumuman { get; set; }

        public int TotalJemaat { get => TotalAnak + TotalRemaja + TotalPemuda + TotalDewasa + TotalLansia; }

        public int TotalAnak { get; set; }
        public int TotalRemaja { get; set; }
        public int TotalPemuda { get; set; }
        public int TotalDewasa { get; set; }
        public int TotalLansia { get; set; }
    }
}
