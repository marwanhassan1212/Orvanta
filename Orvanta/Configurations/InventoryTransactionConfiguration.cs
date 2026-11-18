
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class InventoryTransactionConfiguration
        : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.ToTable("InventoryTransactions", table =>
            {
                table.HasCheckConstraint(
                    "CK_InventoryTransactions_Quantity",
                    "[Quantity] > 0");

                table.HasCheckConstraint(
                    "CK_InventoryTransactions_Source",
                    @"(
                        [TransactionType] = 'OpeningBalance'
                        AND [PurchaseInvoiceItemId] IS NULL
                        AND [SalesInvoiceItemId] IS NULL
                    )
                    OR
                    (
                        [TransactionType] = 'PurchaseIn'
                        AND [PurchaseInvoiceItemId] IS NOT NULL
                        AND [SalesInvoiceItemId] IS NULL
                    )
                    OR
                    (
                        [TransactionType] = 'SaleOut'
                        AND [SalesInvoiceItemId] IS NOT NULL
                        AND [PurchaseInvoiceItemId] IS NULL
                    )");
            });

            builder.HasKey(t => t.Id);

            builder.Property(t => t.TransactionType).IsRequired().HasConversion<string>().HasMaxLength(30);
            builder.Property(t => t.Quantity).HasPrecision(18, 3).IsRequired();
            builder.Property(t => t.UnitCost).HasPrecision(18, 4).IsRequired();
            builder.Property(t => t.TotalCost).HasPrecision(18, 2).IsRequired();
            builder.Property(t => t.CreatedByUserId).IsRequired().HasMaxLength(450);
            builder.Property(t => t.CreatedAt).IsRequired();

            builder.HasIndex(t => t.PurchaseInvoiceItemId).IsUnique()
                .HasFilter("[PurchaseInvoiceItemId] IS NOT NULL");

            builder.HasIndex(t => t.SalesInvoiceItemId).IsUnique()
                .HasFilter("[SalesInvoiceItemId] IS NOT NULL");

            builder.HasOne(t => t.Product).WithMany(p => p.InventoryTransactions)
                .HasForeignKey(t => t.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Warehouse).WithMany(w => w.InventoryTransactions)
                .HasForeignKey(t => t.WarehouseId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.PurchaseInvoiceItem).WithOne(i => i.InventoryTransaction)
                .HasForeignKey<InventoryTransaction>(t => t.PurchaseInvoiceItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.SalesInvoiceItem).WithOne(i => i.InventoryTransaction)
                .HasForeignKey<InventoryTransaction>(t => t.SalesInvoiceItemId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
