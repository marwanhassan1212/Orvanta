using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class CustomerReceiptConfiguration : IEntityTypeConfiguration<CustomerReceipt>

    {
        public void Configure(EntityTypeBuilder<CustomerReceipt> builder)
        {
            builder.ToTable("CustomerReceipts", table =>
            {
                table.HasCheckConstraint(
                    "CK_CustomerReceipts_Amount",
                    "[Amount] > 0");
            });

            builder.HasKey(r => r.Id);

            builder.Property(r => r.ReceiptNumber).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Amount).HasPrecision(18, 2).IsRequired();
            builder.Property(r => r.ReceiptDate).IsRequired();
            builder.Property(r => r.Status).IsRequired().HasConversion<string>().HasMaxLength(30);
            builder.Property(r => r.Notes).HasMaxLength(1000);
            builder.Property(r => r.VoidReason).HasMaxLength(1000);
            builder.Property(r => r.CreatedByUserId).IsRequired().HasMaxLength(450);
            builder.Property(r => r.PostedByUserId).HasMaxLength(450);
            builder.Property(r => r.VoidedByUserId).HasMaxLength(450);
            builder.Property(r => r.CreatedAt).IsRequired();
            builder.Property(r => r.PostedAt);
            builder.Property(r => r.VoidedAt);

            builder.HasIndex(r => r.ReceiptNumber).IsUnique();


            builder.HasOne(r => r.Customer).WithMany(c => c.CustomerReceipts)
                .HasForeignKey(r => r.CustomerId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(r => r.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(r => r.PostedByUserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(r => r.VoidedByUserId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}