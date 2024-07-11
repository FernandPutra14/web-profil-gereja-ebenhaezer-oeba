using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.Domain.Entity
{
    public class Ibadah : BaseEntity
    {
        public string Judul { get; set; } = string.Empty;
        public string Deskripsi { get; set; } = string.Empty;
        public string NasPembimbing { get; set; } = string.Empty;
        public string? Renungan {  get; set; }
        public string Tempat { get; set; } = string.Empty;
        public DateTime TanggalIbadah { get; set; }

        public KategoriIbadah? KategoriIbadah { get; set; }
        public Pendeta? Pendeta { get; set; }
    }
}
