using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class PurchaseInvoiceConfiguration : IEntityTypeConfiguration<PurchaseInvoice>

    {
        public void Configure(EntityTypeBuilder<PurchaseInvoice> builder)
        {
            builder.ToTable("PurchaseInvoices", table =>
            {
                table.HasCheckConstraint(
                    "CK_PurchaseInvoices_Amounts",
                    "[Subtotal] >= 0 AND [DiscountAmount] >= 0" +
                    " AND [TotalAmount] >= 0 AND [DiscountAmount] <= [Subtotal]");
            });

            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);
            builder.Property(i => i.InvoiceDate).IsRequired();
            builder.Property(i => i.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
            builder.Property(i => i.Subtotal).HasPrecision(18, 2).IsRequired();
            builder.Property(i => i.DiscountAmount).HasPrecision(18, 2).IsRequired();
            builder.Property(i => i.TotalAmount).HasPrecision(18, 2).IsRequired();
            builder.Property(i => i.Notes).HasMaxLength(1000);
            builder.Property(i => i.LastRejectionReason).HasMaxLength(1000);
            builder.Property(i => i.CreatedByUserId).IsRequired().HasMaxLength(450);
            builder.Property(i => i.ApprovedByUserId).HasMaxLength(450);
            builder.Property(i => i.SubmittedAt);
            builder.Property(i => i.ApprovedAt);
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Property(i => i.UpdatedAt);

            builder.HasIndex(i => i.InvoiceNumber).IsUnique();

            builder.HasOne(i => i.Supplier).WithMany(s => s.PurchaseInvoices)
                .HasForeignKey(i => i.SupplierId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Warehouse).WithMany(w => w.PurchaseInvoices)
                .HasForeignKey(i => i.WarehouseId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Items).WithOne(item => item.PurchaseInvoice)
                .HasForeignKey(item => item.PurchaseInvoiceId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.PaymentAllocations).WithOne(a => a.PurchaseInvoice)
                .HasForeignKey(a => a.PurchaseInvoiceId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}