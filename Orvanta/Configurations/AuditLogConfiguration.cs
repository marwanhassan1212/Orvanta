using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>

    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.UserId).HasMaxLength(450);
            builder.Property(a => a.Action).IsRequired().HasMaxLength(100);
            builder.Property(a => a.EntityName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.EntityId).IsRequired().HasMaxLength(100);
            builder.Property(a => a.OldValues);
            builder.Property(a => a.NewValues);
            builder.Property(a => a.IpAddress).HasMaxLength(45);
            builder.Property(a => a.CreatedAt).IsRequired();

            builder.HasIndex(a => a.CreatedAt);
            builder.HasIndex(a => new
            {
                a.EntityName,
                a.EntityId
            });

            builder.HasOne(a => a.User).WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Restrict);         
        }
    }
}