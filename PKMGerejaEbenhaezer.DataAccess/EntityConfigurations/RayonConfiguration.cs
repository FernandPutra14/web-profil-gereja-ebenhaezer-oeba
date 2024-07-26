using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKMGerejaEbenhaezer.DataAccess.ValueConverters;
using PKMGerejaEbenhaezer.Domain.Entity;

namespace PKMGerejaEbenhaezer.DataAccess.EntityConfigurations;

public class RayonConfiguration : IEntityTypeConfiguration<Rayon>
{
    public void Configure(EntityTypeBuilder<Rayon> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasOne(e => e.FotoKetua).WithMany().OnDelete(DeleteBehavior.SetNull);
        builder.Property(r => r.NoWa).HasConversion<NoWaValueConverter>();
    }
}
