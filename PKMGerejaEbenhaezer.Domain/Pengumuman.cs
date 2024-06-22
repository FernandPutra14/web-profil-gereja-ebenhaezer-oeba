using PKMGerejaEbenhaezer.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PKMGerejaEbenhaezer.Domain
{
    public class Pengumuman : BaseEntity
    {
        public string Judul { get; set; }
        public string Keterangan { get; set; }
        public string PathFoto { get; set; }
        public DateTime Tanggal {  get; set; }
    }
}
