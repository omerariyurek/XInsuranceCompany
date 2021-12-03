using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace XInsuranceCompany.WebUI.Models.InsuranceOffer
{
    public class CreateInsuranceOfferModel
    {
        [Required(ErrorMessage = "TC identity number is required!")]
        public string TcIdentityNo { get; set; }
        [Required(ErrorMessage = "Plate no is required!")]
        public string PlateNo { get; set; }
        [Required(ErrorMessage = "License serial code is required!")]
        public string LicenseSerialCode { get; set; }
        [Required(ErrorMessage = "License serial number is required!")]
        public string LicenseSerialNumber { get; set; }
    }
}
