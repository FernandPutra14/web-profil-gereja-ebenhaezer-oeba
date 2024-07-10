using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKMGerejaEbenhaezer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.DataAccess.EntityConfigurations
{
    public class KategoriIbadahConfiguration : IEntityTypeConfiguration<KategoriIbadah>
    {
        public void Configure(EntityTypeBuilder<KategoriIbadah> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Warna)
                .HasConversion(w => w.ToArgb(), argb => Color.FromArgb(argb));
        }
    }
}
