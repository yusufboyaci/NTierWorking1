using Core.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping
{
    public class CoreMap<T> : IEntityTypeConfiguration<T> where T : CoreEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Status).IsRequired(true);

            builder.Property(x => x.CreatedComputerName).HasColumnName("Created Computer Name").IsRequired(false);
            builder.Property(x => x.CreatedDate).HasColumnName("Created Date").IsRequired(false);
            builder.Property(x => x.CreatedADUserName).HasColumnName("Created AD User Name").IsRequired(false);
            builder.Property(x => x.CreatedBy).HasColumnName("Created By").IsRequired(false);
            builder.Property(x => x.CreatedIP).HasColumnName("Created IP").IsRequired(false);


            builder.Property(x => x.ModifiedComputerName).HasColumnName("Modified Computer Name").IsRequired(false);
            builder.Property(x => x.ModifiedDate).HasColumnName("Modified Date").IsRequired(false);
            builder.Property(x => x.ModifiedADUserName).HasColumnName("Modified AD User Name").IsRequired(false);
            builder.Property(x => x.ModifiedBy).HasColumnName("Modified By").IsRequired(false);
            builder.Property(x => x.ModifiedIP).HasColumnName("Modified IP").IsRequired(false);
        }
    }
}
