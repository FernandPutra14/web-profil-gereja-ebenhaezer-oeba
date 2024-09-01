using PKMGerejaEbenhaezer.Domain.Entity.Commons;
using PKMGerejaEbenhaezer.Domain.ValueObjects;
using System;

namespace PKMGerejaEbenhaezer.Domain.Entity;

public class Ibadah : BaseEntity
{
    public string Judul { get; set; } = string.Empty;
    public string Deskripsi { get; set; } = string.Empty;

    public AyatAlkitab NatsPembimbing { get; set; }
    public string IsiNatsPembimbing { get; set; } = string.Empty;

    public AyatAlkitab? Bacaan {  get; set; }
    public string? IsiBacaan { get; set; }

    public string Tempat { get; set; } = string.Empty;
    public DateTime TanggalIbadah { get; set; }

    public KategoriIbadah? KategoriIbadah { get; set; }
    public Pendeta? Pendeta { get; set; }
}
