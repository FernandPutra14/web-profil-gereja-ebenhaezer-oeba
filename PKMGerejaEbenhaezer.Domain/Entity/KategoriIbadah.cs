using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.Domain.Entity
{
    public class KategoriIbadah : BaseEntity
    {
        public string Nama { get; set; } = string.Empty;
        public TimeSpan Durasi { get; set; }

        public List<Ibadah> DaftarIbadah { get; set; } = new();
    }
}
