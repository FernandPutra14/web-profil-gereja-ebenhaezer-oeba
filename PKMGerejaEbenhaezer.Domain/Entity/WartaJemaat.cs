using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using PKMGerejaEbenhaezer.Domain.Entity.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PKMGerejaEbenhaezer.Domain.Entity
{
    public class WartaJemaat : BaseEntity, IAuditableEntity
    {
        public DateTime TanggalWarta { get; set; }
        public Uri DocumentLink { get; set; }

        public DateTime TanggalDiBuat { get; set ; }
        public DateTime? TanggalDiUbah { get; set; }
        public AppUser? Pembuat { get; set; }
    }
}
