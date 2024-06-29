using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using PKMGerejaEbenhaezer.Domain.Entity.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PKMGerejaEbenhaezer.Domain.Entity
{
    public class Pengumuman : BaseEntity, IAuditableEntity
    {
        public string Judul { get; set; }
        public string Isi { get; set; }

        public Foto? Foto { get; set; }

        public DateTime TanggalDiBuat { get; set ; }
        public DateTime? TanggalDiUbah { get ; set ; }

        public AppUser? Pembuat { get; set; }
    }
}
