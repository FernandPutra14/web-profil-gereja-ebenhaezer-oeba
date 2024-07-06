using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKMGerejaEbenhaezer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.DataAccess.EntityConfigurations
{
    public class RayonConfiguration : IEntityTypeConfiguration<Rayon>
    {
        public void Configure(EntityTypeBuilder<Rayon> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(e => e.FotoKetua).WithMany().OnDelete(DeleteBehavior.SetNull);
        }
    }
}
