using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(s => s.Id);


            builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Phone).HasMaxLength(30);
            builder.Property(s => s.Email).HasMaxLength(256);
            builder.Property(s => s.Address).HasMaxLength(500);
            builder.Property(s => s.Notes).HasMaxLength(1000);
            builder.Property(s => s.IsActive).IsRequired();
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.UpdatedAt);

            builder.HasMany(s => s.PurchaseInvoices).WithOne(i => i.Supplier)
                .HasForeignKey(i => i.SupplierId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.SupplierPayments).WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}