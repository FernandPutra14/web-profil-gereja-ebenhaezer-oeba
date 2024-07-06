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
    public class KategoriIbadahConfiguration : IEntityTypeConfiguration<KategoriIbadah>
    {
        public void Configure(EntityTypeBuilder<KategoriIbadah> builder)
        {
            builder.HasKey(k => k.Id);
        }
    }
}
