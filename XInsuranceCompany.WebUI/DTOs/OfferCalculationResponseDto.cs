using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XInsuranceCompany.WebUI.DTOs
{
    public class OfferCalculationResponseDto
    {
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public string OfferDescription { get; set; }
        public decimal OfferPrice { get; set; }
        public string PlateNo { get; set; }
    }
}
