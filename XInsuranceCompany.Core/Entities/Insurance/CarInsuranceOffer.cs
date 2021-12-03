using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using XInsuranceCompany.Core.Entities.Common;

namespace XInsuranceCompany.Core.Entities.Insurance
{
    public partial class CarInsuranceOffer : IBaseEntity, ISoftDeletedEntity
    {
        public CarInsuranceOffer()
        {
            CarInsuranceOfferDetails = new List<CarInsuranceOfferDetail>();
        }
        public int Id { get; set; }
        public string IdentificationNumber { get; set; }
        public string Plate { get; set; }
        public string LicenseSerialCode { get; set; }
        public string LicenseSerialNumber { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<CarInsuranceOfferDetail> CarInsuranceOfferDetails { get; set; }
        [NotMapped]
        public string EncryptedId { get; set; }
    }
}
