using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", table =>
            {
                table.HasCheckConstraint(
                    "CK_Products_PurchasePrice",
                    "[PurchasePrice] >= 0");

                table.HasCheckConstraint(
                    "CK_Products_SellingPrice",
                    "[SellingPrice] >= 0");
            });

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.SKU).IsRequired().HasMaxLength(100);
            builder.HasIndex(p => p.SKU).IsUnique();
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.PurchasePrice).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.SellingPrice).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt);

            builder.HasOne(p => p.Category).WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Unit).WithMany(u => u.Products).HasForeignKey(p => p.UnitId)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.InventoryStocks).WithOne(s => s.Product).HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.InventoryTransactions).WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.SalesInvoiceItems).WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.PurchaseInvoiceItems).WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}