using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using PKMGerejaEbenhaezer.Domain.Entity.Contracts;
using System;

namespace PKMGerejaEbenhaezer.Domain.Entity;

public class Foto : BaseEntity, IAuditableEntity
{
    public string PathFoto { get; set; } = string.Empty;
    public string PathFotoSmall { get; set; } = string.Empty;
    public string PathFotoMedium { get; set; } = string.Empty;
    public string PathFotoLarge { get; set; } = string.Empty;

    public DateTime TanggalDiBuat { get; set; }
    public DateTime? TanggalDiUbah { get; set; }

    public AppUser? Pembuat { get; set; }
}
