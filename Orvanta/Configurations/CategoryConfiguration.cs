using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt);

            builder.HasIndex(c => c.Name).IsUnique();

            builder.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}