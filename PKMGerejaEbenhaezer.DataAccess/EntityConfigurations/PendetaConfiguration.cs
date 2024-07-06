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
    public class PendetaConfiguration : IEntityTypeConfiguration<Pendeta>
    {
        public void Configure(EntityTypeBuilder<Pendeta> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Foto).WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasIndex(p => p.Nama).IsUnique();
            builder.Property(p => p.FacebookProfileLink)
                .HasConversion(l => l.ToString(), l => new Uri(l));
            builder.Property(p => p.InstagramProfileLink)
                .HasConversion(l => l.ToString(), l => new Uri(l));
            builder.Property(p => p.YoutubeProfileLink)
                .HasConversion(l => l.ToString(), l => new Uri(l));
        }
    }
}
