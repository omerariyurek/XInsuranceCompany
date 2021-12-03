using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XInsuranceCompany.Service.CarInsuranceOffer
{
    public interface ICarInsuranceOfferService
    {
        Task<Core.Entities.Insurance.CarInsuranceOffer> GetOfferByPlateNoAndIdentificationNumberAsync(
            string identificationNumber, string plateNo);
        Task<Core.Entities.Insurance.CarInsuranceOffer> CreateInsuranceOfferAsync(Core.Entities.Insurance.CarInsuranceOffer carInsuranceOffer);
        Task<List<Core.Entities.Insurance.CarInsuranceOffer>> GetOffersByIdentityNoAsync(string identityNo);
        Task<Core.Entities.Insurance.CarInsuranceOffer> GetOfferDetailByIdAsync(int id);
    }
}
