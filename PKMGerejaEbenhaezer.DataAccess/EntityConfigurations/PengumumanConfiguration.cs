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
    public class PengumumanConfiguration : IEntityTypeConfiguration<Pengumuman>
    {
        public void Configure(EntityTypeBuilder<Pengumuman> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Foto).WithMany().OnDelete(DeleteBehavior.SetNull);
        }
    }
}
