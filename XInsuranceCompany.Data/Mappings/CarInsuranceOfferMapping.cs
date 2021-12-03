using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XInsuranceCompany.Core.Entities.Insurance;

namespace XInsuranceCompany.Data.Mappings
{
    public class CarInsuranceOfferMapping : IEntityTypeConfiguration<CarInsuranceOffer>
    {

        public void Configure(EntityTypeBuilder<CarInsuranceOffer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IdentificationNumber).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Plate).IsRequired().HasMaxLength(20);
            builder.Property(x => x.LicenseSerialCode).IsRequired().HasMaxLength(500);
            builder.Property(x => x.LicenseSerialNumber).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Deleted).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.ToTable("CarInsuranceOffers");
        }
    }
}
