﻿using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly HttpClient _client;

        public StripePaymentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<SuccessModel> CheckOutCompleted(StripePaymentDTO model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            //post request to create a new stripe session
            var response = await _client.PostAsync("api/stripepayment/create", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SuccessModel>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
