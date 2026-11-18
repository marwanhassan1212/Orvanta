using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Phone).HasMaxLength(30);
            builder.Property(c => c.Email).HasMaxLength(256);
            builder.Property(c => c.Address).HasMaxLength(500);
            builder.Property(c => c.Notes).HasMaxLength(1000);
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt);

            builder.HasMany(c => c.SalesInvoices).WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CustomerReceipts).WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}