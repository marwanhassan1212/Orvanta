using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class SalesInvoiceItemConfiguration : IEntityTypeConfiguration<SalesInvoiceItem>
    {
        public void Configure(EntityTypeBuilder<SalesInvoiceItem> builder)
        {
            builder.ToTable("SalesInvoiceItems", table =>
            {
                table.HasCheckConstraint(
                    "CK_SalesInvoiceItems_Amounts",
                    "[Quantity] > 0 AND [UnitPrice] >= 0 AND [LineTotal] >= 0 AND ([UnitCostSnapshot] IS NULL OR [UnitCostSnapshot] >= 0)");
            });

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Quantity).HasPrecision(18, 3).IsRequired();
            builder.Property(i => i.UnitPrice).HasPrecision(18, 2).IsRequired();
            builder.Property(i => i.UnitCostSnapshot).HasPrecision(18, 4);
            builder.Property(i => i.LineTotal).HasPrecision(18, 2).IsRequired();

            builder.HasIndex(i => new { i.SalesInvoiceId, i.ProductId }).IsUnique();

            builder.HasOne(i => i.SalesInvoice).WithMany(i => i.Items)
                .HasForeignKey(i => i.SalesInvoiceId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Product).WithMany(p => p.SalesInvoiceItems)
                .HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);

           builder.HasOne(i => i.InventoryTransaction).WithOne(t => t.SalesInvoiceItem)
                .HasForeignKey<InventoryTransaction>(t => t.SalesInvoiceItemId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}