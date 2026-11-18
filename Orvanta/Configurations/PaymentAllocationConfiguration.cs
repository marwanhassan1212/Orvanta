using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class PaymentAllocationConfiguration : IEntityTypeConfiguration<PaymentAllocation>

    {
        public void Configure(EntityTypeBuilder<PaymentAllocation> builder)
        {
            builder.ToTable("PaymentAllocations", table =>
            {
                table.HasCheckConstraint(
                    "CK_PaymentAllocations_Amount",
                    "[AllocatedAmount] > 0");
            });

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AllocatedAmount).HasPrecision(18, 2).IsRequired();

            builder.HasIndex(a => new
            {
                a.SupplierPaymentId,
                a.PurchaseInvoiceId
            }).IsUnique();

            builder.HasOne(a => a.SupplierPayment).WithMany(p => p.Allocations)
                .HasForeignKey(a => a.SupplierPaymentId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PurchaseInvoice).WithMany(i => i.PaymentAllocations)
                .HasForeignKey(a => a.PurchaseInvoiceId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}