using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>

    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses", table =>
            {
                table.HasCheckConstraint(
                    "CK_Expenses_Amount",
                    "[Amount] > 0");
            });

            builder.HasKey(e => e.Id);

            builder.Property(e => e.ExpenseNumber).IsRequired().HasMaxLength(50);    
            builder.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
            builder.Property(e => e.ExpenseDate).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
            builder.Property(e => e.Notes).HasMaxLength(1000);            
            builder.Property(e => e.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
            builder.Property(e => e.VoidReason).HasMaxLength(1000);
            builder.Property(e => e.CreatedByUserId).IsRequired().HasMaxLength(450);
            builder.Property(e => e.PostedByUserId).HasMaxLength(450);
            builder.Property(e => e.VoidedByUserId).HasMaxLength(450);
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.PostedAt);
            builder.Property(e => e.VoidedAt);

            builder.HasIndex(e => e.ExpenseNumber).IsUnique();

            builder.HasOne(e => e.ExpenseCategory).WithMany(c => c.Expenses)
                .HasForeignKey(e => e.ExpenseCategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(e => e.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(e => e.PostedByUserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(e => e.VoidedByUserId).OnDelete(DeleteBehavior.Restrict);

        }

    }
}