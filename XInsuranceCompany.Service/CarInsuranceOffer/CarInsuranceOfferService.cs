using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using XInsuranceCompany.Core.Security;
using XInsuranceCompany.Data.Abstract;

namespace XInsuranceCompany.Service.CarInsuranceOffer
{
    public class CarInsuranceOfferService : ICarInsuranceOfferService
    {
        private readonly ICarInsuranceOfferRepository _carInsuranceOfferRepository;
        private readonly IEncryption _encryption;

        public CarInsuranceOfferService(ICarInsuranceOfferRepository carInsuranceOfferRepository, IEncryption encryption)
        {
            _carInsuranceOfferRepository = carInsuranceOfferRepository;
            _encryption = encryption;
        }

        public async Task<Core.Entities.Insurance.CarInsuranceOffer> GetOfferByPlateNoAndIdentificationNumberAsync(string identificationNumber, string plateNo)
        {
            var encryptedTCno = _encryption.EncryptText(identificationNumber);
            var data= await _carInsuranceOfferRepository.GetAsync(x =>
                !x.Deleted && x.IdentificationNumber == encryptedTCno && x.Plate == plateNo);
            if (data == null)
                return null;
            data.IdentificationNumber = _encryption.DecryptText(data.IdentificationNumber);
            data.LicenseSerialCode = _encryption.DecryptText(data.LicenseSerialCode);
            data.LicenseSerialNumber = _encryption.DecryptText(data.LicenseSerialNumber);
            return data;
        }

        public async Task<Core.Entities.Insurance.CarInsuranceOffer> CreateInsuranceOfferAsync(Core.Entities.Insurance.CarInsuranceOffer carInsuranceOffer)
        {
            carInsuranceOffer.IdentificationNumber = _encryption.EncryptText(carInsuranceOffer.IdentificationNumber);
            carInsuranceOffer.LicenseSerialCode = _encryption.EncryptText(carInsuranceOffer.LicenseSerialCode);
            carInsuranceOffer.LicenseSerialNumber = _encryption.EncryptText(carInsuranceOffer.LicenseSerialNumber);
            return await _carInsuranceOfferRepository.AddAsync(carInsuranceOffer);
        }

        public async Task<List<Core.Entities.Insurance.CarInsuranceOffer>> GetOffersByIdentityNoAsync(string identityNo)
        {
            var encryptedTCno = _encryption.EncryptText(identityNo);
            var data = await _carInsuranceOfferRepository.GetListAsync(x => !x.Deleted && x.IdentificationNumber == encryptedTCno);
            return data.ToList();
        }

        public async Task<Core.Entities.Insurance.CarInsuranceOffer> GetOfferDetailByIdAsync(int id)
        {
            var offer = await _carInsuranceOfferRepository.GetAsync(x => !x.Deleted && x.Id == id,
                "CarInsuranceOfferDetails");
            if (offer == null) 
                return null;

            offer.IdentificationNumber = _encryption.DecryptText(offer.IdentificationNumber);
            offer.LicenseSerialCode = _encryption.DecryptText(offer.LicenseSerialCode);
            offer.LicenseSerialNumber = _encryption.DecryptText(offer.LicenseSerialNumber);
            return offer;
        }
    }
}
