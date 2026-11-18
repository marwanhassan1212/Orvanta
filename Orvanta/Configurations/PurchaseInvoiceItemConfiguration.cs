using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class PurchaseInvoiceItemConfiguration
        : IEntityTypeConfiguration<PurchaseInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseInvoiceItem> builder)
        {
            builder.ToTable("PurchaseInvoiceItems", table =>
            {
                table.HasCheckConstraint(
                    "CK_PurchaseInvoiceItems_Amounts",
                    "[Quantity] > 0 AND [UnitPrice] >= 0 " +
                    "AND [AllocatedDiscount] >= 0 AND [LineTotal] >= 0 AND [UnitCost] >= 0");
            });

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantity).HasPrecision(18, 3).IsRequired();
            builder.Property(i => i.UnitPrice).HasPrecision(18, 2).IsRequired();
            builder.Property(i => i.AllocatedDiscount).HasPrecision(18, 2).IsRequired();
            builder.Property(i => i.LineTotal).HasPrecision(18, 2).IsRequired();
            builder.Property(i => i.UnitCost).HasPrecision(18, 4).IsRequired();

            builder.HasIndex(i => new { i.PurchaseInvoiceId, i.ProductId }).IsUnique();

            builder.HasOne(i => i.PurchaseInvoice).WithMany(i => i.Items)
                .HasForeignKey(i => i.PurchaseInvoiceId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Product).WithMany(p => p.PurchaseInvoiceItems)
                .HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.InventoryTransaction).WithOne(t => t.PurchaseInvoiceItem)
                .HasForeignKey<InventoryTransaction>(t => t.PurchaseInvoiceItemId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}