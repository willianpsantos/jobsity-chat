using Jobsity.Chat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobsity.Chat.DB.Mappings
{
    internal class JobsityEntityMappings<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : JobsityModel
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(256);

            builder
                .Property(x => x.CreatedAt)
                .HasColumnType("datetimeoffset")
                .HasDefaultValueSql("(sysutcdatetime())")
                .IsRequired();

            builder
                .Property(x => x.UpdatedAt)
                .HasColumnType("datetimeoffset")
                .IsRequired(false);

            builder
                .Property(x => x.CreatedBy)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .Property(x => x.UpdatedBy)
                .IsRequired(false)
                .HasMaxLength(256);
        }
    }
}
