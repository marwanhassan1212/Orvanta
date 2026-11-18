using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class SupplierPaymentConfiguration : IEntityTypeConfiguration<SupplierPayment>

    {
        public void Configure(EntityTypeBuilder<SupplierPayment> builder)
        {
            builder.ToTable("SupplierPayments", table =>
            {
                table.HasCheckConstraint(
                    "CK_SupplierPayments_Amount",
                    "[Amount] > 0");
            });

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(p => p.PaymentNumber).IsUnique();
            builder.Property(p => p.Amount).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.PaymentDate).IsRequired();
            builder.Property(p => p.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
            builder.Property(p => p.Notes).HasMaxLength(1000);
            builder.Property(p => p.VoidReason).HasMaxLength(1000);
            builder.Property(p => p.CreatedByUserId).IsRequired().HasMaxLength(450);
            builder.Property(p => p.PostedByUserId).HasMaxLength(450);
            builder.Property(p => p.VoidedByUserId).HasMaxLength(450);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.PostedAt);
            builder.Property(p => p.VoidedAt);

            builder.HasOne(p => p.Supplier).WithMany(s => s.SupplierPayments)
                .HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(p => p.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(p => p.PostedByUserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(p => p.VoidedByUserId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}