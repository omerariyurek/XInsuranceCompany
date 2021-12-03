using System;
using System.Collections.Generic;
using System.Text;
using XInsuranceCompany.Core.Entities.Common;

namespace XInsuranceCompany.Core.Entities.Insurance
{
    public class CarInsuranceOfferDetail : IBaseEntity
    {
        public int Id { get; set; }
        public int CarInsuranceOfferId { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string InsuranceCompanyLogo { get; set; }
        public string OfferDescription { get; set; }
        public decimal OfferPrice { get; set; }
        public virtual CarInsuranceOffer CarInsuranceOffer { get; set; }
    }
}
