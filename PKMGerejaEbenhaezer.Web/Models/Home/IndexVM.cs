using PKMGerejaEbenhaezer.Domain.Entity;

namespace PKMGerejaEbenhaezer.Web.Models.Home
{
    public class IndexVM
    {
        public List<Pengumuman> DaftarPengumuman { get; set; } = new();
        public List<WartaJemaat> DaftarWartaJemaat { get; set; } = new();
        public List<Pendeta> DaftarPendeta { get; set; } = new();
        public List<Ibadah> DaftarIbadah { get; set; } = new();
    }
}
