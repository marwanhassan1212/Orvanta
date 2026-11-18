using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>

    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
        {
            builder.ToTable("ExpenseCategories");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(150);
            builder.HasIndex(e => e.Name).IsUnique();
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasMany(e => e.Expenses).WithOne(e => e.ExpenseCategory)
                .HasForeignKey(e => e.ExpenseCategoryId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}