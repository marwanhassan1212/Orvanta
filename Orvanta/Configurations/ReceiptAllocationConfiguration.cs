using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class ReceiptAllocationConfiguration : IEntityTypeConfiguration<ReceiptAllocation>

    {
        public void Configure(EntityTypeBuilder<ReceiptAllocation> builder)
        {
            builder.ToTable("ReceiptAllocations", table =>
            {
                table.HasCheckConstraint(
                    "CK_ReceiptAllocations_Amount",
                    "[AllocatedAmount] > 0");
            });

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AllocatedAmount).HasPrecision(18, 2).IsRequired();

            builder.HasIndex(a => new
            {
                a.CustomerReceiptId,
                a.SalesInvoiceId
            }).IsUnique();

            builder.HasOne(a => a.CustomerReceipt).WithMany(r => r.Allocations)
                .HasForeignKey(a => a.CustomerReceiptId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.SalesInvoice).WithMany(i => i.ReceiptAllocations)
                .HasForeignKey(a => a.SalesInvoiceId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}