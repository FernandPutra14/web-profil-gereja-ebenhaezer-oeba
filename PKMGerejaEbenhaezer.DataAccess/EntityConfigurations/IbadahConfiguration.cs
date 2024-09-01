using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.DataAccess.EntityConfigurations
{
    public class IbadahConfiguration : IEntityTypeConfiguration<Ibadah>
    {
        public void Configure(EntityTypeBuilder<Ibadah> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.KategoriIbadah)
                .WithMany(k => k.DaftarIbadah).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(i => i.Pendeta)
                .WithMany(p => p.DaftarIbadah).OnDelete(DeleteBehavior.SetNull);
            builder.Property(i => i.TanggalIbadah)
                .HasColumnType("timestamp without time zone");
            builder.Property(i => i.NatsPembimbing)
                .HasConversion(i => i.ToString(), s => AyatAlkitab.Parse(s, null));
            builder.Property(i => i.Bacaan)
                .HasConversion(i => i.ToString(), s => AyatAlkitab.Parse(s, null));
        }
    }
}
