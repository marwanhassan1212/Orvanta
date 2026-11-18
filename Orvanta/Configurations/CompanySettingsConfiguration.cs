using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orvanta.Models;

namespace Orvanta.Data.Configurations
{
    public class CompanySettingConfiguration : IEntityTypeConfiguration<CompanySetting>

    {
        public void Configure(EntityTypeBuilder<CompanySetting> builder)
        {
            builder.ToTable("CompanySettings", table =>
            {
                table.HasCheckConstraint(
                    "CK_CompanySettings_SingleRecord",
                    "[Id] = 1");
            }); ;

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Phone).HasMaxLength(30);
            builder.Property(c => c.Email).HasMaxLength(256);
            builder.Property(c => c.Address).HasMaxLength(500);
            builder.Property(c => c.CurrencyCode).IsRequired().HasMaxLength(3);
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt);

        }
    }
}