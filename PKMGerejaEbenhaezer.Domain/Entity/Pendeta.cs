using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PKMGerejaEbenhaezer.Domain.Entity
{
    public class Pendeta : BaseEntity
    {
        public string Nama { get; set; }
        public Foto? Foto { get; set; }

        public Uri? FacebookProfileLink { get; set; }
        public Uri? InstagramProfileLink { get; set; }
        public Uri? YoutubeProfileLink { get; set; }

        public List<Ibadah> DaftarIbadah { get; set; } = new();
    }
}
