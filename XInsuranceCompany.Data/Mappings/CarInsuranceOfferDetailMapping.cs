using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XInsuranceCompany.Core.Entities.Insurance;

namespace XInsuranceCompany.Data.Mappings
{
    public class CarInsuranceOfferDetailMapping : IEntityTypeConfiguration<CarInsuranceOfferDetail>
    {
        public void Configure(EntityTypeBuilder<CarInsuranceOfferDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.OfferPrice).IsRequired();
            builder.Property(x => x.InsuranceCompanyName).IsRequired().HasMaxLength(500);
            builder.Property(x => x.OfferDescription).HasMaxLength(1000);

        }
    }
}
