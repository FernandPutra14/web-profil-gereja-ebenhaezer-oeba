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
    public class WartaJemaatConfiguration : IEntityTypeConfiguration<WartaJemaat>
    {
        public void Configure(EntityTypeBuilder<WartaJemaat> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.DocumentLink)
                .HasConversion(l => l.ToString(), l => new Uri(l));
            builder.HasIndex(w => w.TanggalWarta);
        }
    }
}
