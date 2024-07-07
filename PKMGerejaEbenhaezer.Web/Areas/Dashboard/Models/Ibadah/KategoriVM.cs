using PKMGerejaEbenhaezer.Domain.Entity;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Ibadah
{
    public class KategoriVM
    {
        public List<KategoriIbadah> Items { get; set; }
        public TambahKategoriVM TambahKategoriVM { get; set; }
    }
}
