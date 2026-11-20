using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Symbol).IsRequired().HasMaxLength(20);
            builder.Property(u => u.IsActive).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.Symbol).IsUnique();

            builder.HasMany(u => u.Products).WithOne(p => p.Unit).HasForeignKey(p => p.UnitId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}