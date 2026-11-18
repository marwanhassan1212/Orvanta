using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class InventoryStockConfiguration : IEntityTypeConfiguration<InventoryStock>
    {
        public void Configure(EntityTypeBuilder<InventoryStock> builder)
        {
            builder.ToTable("InventoryStocks", table =>
            {
                table.HasCheckConstraint(
                    "CK_InventoryStocks_Quantity",
                    "[Quantity] >= 0");

                table.HasCheckConstraint(
                    "CK_InventoryStocks_AverageCost",
                    "[AverageCost] >= 0");
            });

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Quantity).HasPrecision(18, 3).IsRequired();
            builder.Property(s => s.AverageCost).HasPrecision(18, 4).IsRequired();
            builder.Property(s => s.RowVersion).IsRowVersion();
            builder.Property(s => s.UpdatedAt).IsRequired();

            builder.HasIndex(s => new { s.ProductId, s.WarehouseId }).IsUnique();

            builder.HasOne(s => s.Product).WithMany(p => p.InventoryStocks)
                .HasForeignKey(s => s.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Warehouse).WithMany(w => w.InventoryStocks)
                .HasForeignKey(s => s.WarehouseId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}