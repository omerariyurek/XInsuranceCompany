using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using XInsuranceCompany.Core.Entities.Insurance;
using XInsuranceCompany.Service.CarInsuranceOffer;
using XInsuranceCompany.WebUI.Models.InsuranceOffer;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using XInsuranceCompany.Core.Security;
using XInsuranceCompany.WebUI.ApiService;
using XInsuranceCompany.WebUI.DTOs;
using XInsuranceCompany.WebUI.Models;

namespace XInsuranceCompany.WebUI.Controllers
{
    public class InsuranceOfferController : Controller
    {
        private readonly ICarInsuranceOfferService _carInsuranceOfferService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDataProtector _dataProtector;

        public InsuranceOfferController(ICarInsuranceOfferService carInsuranceOfferService, IWebHostEnvironment hostingEnvironment, IDataProtectionProvider dataProtector)
        {
            _carInsuranceOfferService = carInsuranceOfferService;
            _hostingEnvironment = hostingEnvironment;
            _dataProtector = dataProtector.CreateProtector("offerProtector");
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInsuranceOffer(CreateInsuranceOfferModel createInsuranceOfferModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var newInsuranceOffer = new CarInsuranceOffer
            {
                CreatedDate = DateTime.Now,
                Deleted = false,
                Plate = createInsuranceOfferModel.PlateNo.Trim().ToUpper(),
                IdentificationNumber = createInsuranceOfferModel.TcIdentityNo,
                LicenseSerialCode = createInsuranceOfferModel.LicenseSerialCode.ToUpper(),
                LicenseSerialNumber = createInsuranceOfferModel.LicenseSerialNumber.ToUpper()
            };
            var jsonStr = System.IO.File.ReadAllText(_hostingEnvironment.ContentRootPath + "\\App_Data\\insuranceCompanies.json");
            var insuranceCompanies = JsonConvert.DeserializeObject<List<InsuranceCompanyList>>(jsonStr);
            foreach (var insuranceCompany in insuranceCompanies)
            {
                var offerApiService = new OfferApiService(insuranceCompany.OfferCalculationServiceUrl);
                var responseDto = await offerApiService.OfferCalculationAsync(new OfferCalculationRequestDto
                {
                    TcIdentityNo = createInsuranceOfferModel.TcIdentityNo,
                    PlateNo = createInsuranceOfferModel.PlateNo,
                    LicenseSerialCode = createInsuranceOfferModel.LicenseSerialCode,
                    LicenseSerialNo = createInsuranceOfferModel.LicenseSerialNumber
                });
                if (responseDto == null)
                    continue;
                newInsuranceOffer.CarInsuranceOfferDetails.Add(new CarInsuranceOfferDetail
                {
                    InsuranceCompanyLogo = responseDto.CompanyLogo,
                    InsuranceCompanyName = responseDto.CompanyName,
                    OfferDescription = responseDto.OfferDescription,
                    OfferPrice = responseDto.OfferPrice
                });
            }
            var addedOffer = await _carInsuranceOfferService.CreateInsuranceOfferAsync(newInsuranceOffer);
            addedOffer.EncryptedId = _dataProtector.Protect(addedOffer.Id.ToString());//id'sini encrypt edip detay sayfasına redirect ediyorum.
            return RedirectToAction("OfferDetail", "InsuranceOffer", new { id = addedOffer.EncryptedId });
        }

        [HttpGet]
        [Route("GeneratedOffers")]
        public IActionResult GeneratedOffers() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Offers")]
        public async Task<IActionResult> Offers(string tcIdentityNo)
        {
            if (string.IsNullOrEmpty(tcIdentityNo))
            {
                return RedirectToAction("GeneratedOffers");
            }

            var offers= await _carInsuranceOfferService.GetOffersByIdentityNoAsync(tcIdentityNo);
            offers.ForEach(x => x.EncryptedId = _dataProtector.Protect(x.Id.ToString()));//tüm tekliflerin id'lerini encrypt et. detay sayfasına encrypted halini göndereceğiz...
            var viewModel = new OffersViewModel
            {
                Offers = offers.OrderByDescending(x => x.CreatedDate).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        [Route("OfferDetail/{id?}")]
        public async Task<IActionResult> OfferDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            var decryptedId = int.Parse(_dataProtector.Unprotect(id));//encrypt edilmiş id'yi decrypt et...
            var data = await _carInsuranceOfferService.GetOfferDetailByIdAsync(decryptedId);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = new OfferDetailViewModel
            {
                CarInsuranceOffer = data
            };
            return View(viewModel);
        }
    }
}