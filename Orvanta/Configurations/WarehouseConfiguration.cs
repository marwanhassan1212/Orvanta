using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name).IsRequired().HasMaxLength(150);
            builder.Property(w => w.Code).IsRequired().HasMaxLength(50);
            builder.Property(w => w.Address).HasMaxLength(500);
            builder.Property(w => w.IsActive).IsRequired();
            builder.Property(w => w.CreatedAt).IsRequired();
            builder.Property(w => w.UpdatedAt);

            builder.HasIndex(w => w.Code).IsUnique();

            builder.HasMany(w => w.InventoryStocks).WithOne(s => s.Warehouse)
                .HasForeignKey(s => s.WarehouseId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.InventoryTransactions).WithOne(t => t.Warehouse)
                .HasForeignKey(t => t.WarehouseId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.SalesInvoices).WithOne(i => i.Warehouse)
                .HasForeignKey(i => i.WarehouseId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.PurchaseInvoices).WithOne(i => i.Warehouse)
                .HasForeignKey(i => i.WarehouseId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}