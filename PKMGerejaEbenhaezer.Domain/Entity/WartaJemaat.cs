using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using PKMGerejaEbenhaezer.Domain.Entity.Contracts;
using System;

namespace PKMGerejaEbenhaezer.Domain.Entity
{
    public class WartaJemaat : BaseEntity, IAuditableEntity
    {
        public DateOnly TanggalWarta { get; set; }
        public Uri DocumentLink { get; set; }

        public DateTime TanggalDiBuat { get; set ; }
        public DateTime? TanggalDiUbah { get; set; }
        public AppUser? Pembuat { get; set; }
    }
}
