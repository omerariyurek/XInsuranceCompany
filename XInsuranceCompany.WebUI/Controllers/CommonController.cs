using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XInsuranceCompany.Core.Helpers;
using XInsuranceCompany.Service.CarInsuranceOffer;
using XInsuranceCompany.WebUI.Models;

namespace XInsuranceCompany.WebUI.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICarInsuranceOfferService _carInsuranceOfferService;
        private readonly ICommonHelper _commonHelper;

        public CommonController(ICarInsuranceOfferService carInsuranceOfferService, ICommonHelper commonHelper)
        {
            _carInsuranceOfferService = carInsuranceOfferService;
            _commonHelper = commonHelper;
        }

        [HttpPost]
        public async Task<IActionResult> GetOfferDataByPlateNoAndIdentityNo(string identityNo, string plateNo)
        {
            var operationResult = new OperationResult();
            if (string.IsNullOrEmpty(identityNo) || string.IsNullOrEmpty(plateNo))
            {
                operationResult.IsSuccess = false;
                return Json(operationResult);
            }
            var offer = await _carInsuranceOfferService.GetOfferByPlateNoAndIdentificationNumberAsync(identityNo, plateNo);
            if (offer == null)
            {
                operationResult.IsSuccess = false;
                return Json(operationResult);
            }
            operationResult.IsSuccess = true;
            operationResult.Data = new
            {
                LicenseSerialCode= offer.LicenseSerialCode,
                LicenseSerialNumber = offer.LicenseSerialNumber
            };
            return Json(operationResult);
        }

        [HttpPost]
        public IActionResult VerifyTcIdentificationNumber(string tcIdentityNo)
        {
            var isVerified = _commonHelper.VerifyTcIdentificationNumber(tcIdentityNo);
            return Json(isVerified);
        }
    }
}
