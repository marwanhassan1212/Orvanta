using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class SalesInvoiceConfiguration
        : IEntityTypeConfiguration<SalesInvoice>
    {
        public void Configure(EntityTypeBuilder<SalesInvoice> builder)
        {
            builder.ToTable("SalesInvoices", table =>
            {
                table.HasCheckConstraint(
                    "CK_SalesInvoices_Amounts",
                    "[Subtotal] >= 0 AND [DiscountAmount] >= 0 AND [TotalAmount] >= 0 AND [DiscountAmount] <= [Subtotal]");

            });

            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(i => i.InvoiceNumber).IsUnique();
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


            builder.HasOne(i => i.Customer).WithMany(c => c.SalesInvoices)
                .HasForeignKey(i => i.CustomerId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Warehouse).WithMany(w => w.SalesInvoices)
                .HasForeignKey(i => i.WarehouseId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Items).WithOne(item => item.SalesInvoice)
                .HasForeignKey(item => item.SalesInvoiceId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.ReceiptAllocations).WithOne(a => a.SalesInvoice)
                .HasForeignKey(a => a.SalesInvoiceId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}