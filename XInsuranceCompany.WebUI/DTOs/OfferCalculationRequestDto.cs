using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XInsuranceCompany.WebUI.DTOs
{
    public class OfferCalculationRequestDto
    {
        public string TcIdentityNo { get; set; }
        public string PlateNo { get; set; }
        public string LicenseSerialNo { get; set; }
        public string LicenseSerialCode { get; set; }
    }
}
