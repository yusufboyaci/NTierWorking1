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
    public class UyeMap : CoreMap<Uye>
    {
        public override void Configure(EntityTypeBuilder<Uye> builder)
        {
            builder.ToTable("uyeler");
            builder.Property(x => x.Ad).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.Soyad).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.KullaniciAdi).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.KullaniciYorum).HasMaxLength(150).IsRequired(false);
            builder.Property(x => x.MailAdresi).HasMaxLength(250).IsRequired(false);
            builder.Property(x => x.KullaniciResimYolu).IsRequired(false);
            builder.Property(x => x.OnayliMi).IsRequired(true);
            builder.HasMany(x => x.Makaleler).WithOne(x => x.Uye).HasForeignKey(x => x.UyeId);
            base.Configure(builder);
        }
    }
}
