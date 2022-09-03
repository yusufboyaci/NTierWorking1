using Core.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Mapping
{
    public class MakaleMap : CoreMap<Makale>
    {
        public override void Configure(EntityTypeBuilder<Makale> builder)
        {
            builder.ToTable("makaleler");
            builder.Property(x => x.MakaleBasligi).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.MakaleIcerigi).HasMaxLength(10000).IsRequired(false);
            builder.Property(x => x.ResimYolu).IsRequired(false);
            builder.Property(x => x.OkunmaSayisi).IsRequired(true);
            builder.Property(x => x.OnayliMi).IsRequired(true);
            builder.HasOne(x => x.Uye).WithMany(x => x.Makaleler);
            base.Configure(builder);    
        }
    }
}
