using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class InvoiceApprovalHistoryConfiguration : IEntityTypeConfiguration<InvoiceApprovalHistory>

    {
        public void Configure(EntityTypeBuilder<InvoiceApprovalHistory> builder)
        {
            builder.ToTable("InvoiceApprovalHistory");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.InvoiceType).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(h => h.InvoiceId).IsRequired();
            builder.Property(h => h.Action).IsRequired().HasConversion<string>().HasMaxLength(30);
            builder.Property(h => h.PerformedByUserId).IsRequired().HasMaxLength(450);
            builder.Property(h => h.RejectionReason).HasMaxLength(1000);
            builder.Property(h => h.PerformedAt).IsRequired();

            builder.HasIndex(h => new
            {
                h.InvoiceType,
                h.InvoiceId,
                h.PerformedAt
            });

            builder.HasOne<ApplicationUser>().WithMany(u => u.ApprovalHistory)
                .HasForeignKey(h => h.PerformedByUserId).OnDelete(DeleteBehavior.Restrict);
               
        }
    }
}