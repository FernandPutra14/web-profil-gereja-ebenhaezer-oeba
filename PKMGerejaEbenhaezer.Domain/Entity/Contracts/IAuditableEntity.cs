using System;
using System.Collections.Generic;
using System.Text;

namespace PKMGerejaEbenhaezer.Domain.Entity.Contracts
{
    public interface IAuditableEntity
    {
        public DateTime TanggalDiBuat {  get; set; }
        public DateTime? TanggalDiUbah { get; set; }

        public AppUser? Pembuat { get; set; }
    }
}
