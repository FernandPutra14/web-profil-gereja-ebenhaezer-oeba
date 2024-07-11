using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.Domain.Entity
{
    public class KategoriIbadah : BaseEntity
    {
        public string Nama { get; set; } = string.Empty;
        public TimeSpan Durasi { get; set; }
        public Color Warna { get; set; }

        public List<Ibadah> DaftarIbadah { get; set; } = new();
    }

    public static class KategoriColors
    {
        public static readonly Color Warna1 = Color.FromArgb(242, 242, 242);
        public static readonly Color Warna2 = Color.FromArgb(237, 250, 241);
        public static readonly Color Warna3 = Color.FromArgb(250, 242, 233);
        public static readonly Color Warna4 = Color.FromArgb(253, 241, 241);
        public static readonly Color Warna5 = Color.FromArgb(231, 251, 249);
        public static readonly Color Warna6 = Color.FromArgb(251, 241, 255);
        public static readonly Color Warna7 = Color.FromArgb(254, 254, 236);
    }
}
