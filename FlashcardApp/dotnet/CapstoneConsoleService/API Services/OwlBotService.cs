using System;
using System.Collections.Generic;
using System.Text;
using CapstoneClient;
using CapstoneClient.Models;
using RestSharp;

namespace CapstoneClient.API_Services
{
    public class OwlBotService
    {
        private readonly string API_URL = "https://owlbot.info/api/v4/dictionary/";
        private readonly RestClient client = new RestClient();
        private readonly string clientToken = "Token 8ef2bedfce46020cfbc3be88f89c819f02f23a3a";

        public OwlBotResult GetDefinition(string term)
        {
            RestRequest request = new RestRequest(API_URL+term);
            request.AddHeader("Authorization", clientToken);
            IRestResponse<OwlBotResult> response = client.Get<OwlBotResult>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                throw new Exception("Error in connecting to server or unsuccessful response");
            }
            else
            {
                return response.Data;
            }
        }
    }
}
