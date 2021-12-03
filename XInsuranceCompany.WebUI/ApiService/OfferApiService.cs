using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XInsuranceCompany.WebUI.DTOs;

namespace XInsuranceCompany.WebUI.ApiService
{
    public class OfferApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;

        public OfferApiService(string baseAddress)
        {
            _httpClient = new HttpClient {Timeout = TimeSpan.FromSeconds(20)};
            _baseAddress = baseAddress;
        }

        public async Task<OfferCalculationResponseDto> OfferCalculationAsync(
            OfferCalculationRequestDto calculationRequestDto)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_baseAddress);
                var strContent = new StringContent(JsonConvert.SerializeObject(calculationRequestDto), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/offers/calculation", strContent);
                if (!response.IsSuccessStatusCode)
                    return null;

                var responseDto =
                    JsonConvert.DeserializeObject<OfferCalculationResponseDto>(
                        await response.Content.ReadAsStringAsync());
                return responseDto;
            }
            catch (Exception e)
            {
                //TODO LOG
                return null;
            }
            
        }
    }
}
