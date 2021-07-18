using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TenmoClient.Models;

namespace TenmoClient
{
    class ApiService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();

        public ApiService()
        {
            try //catch an invalid username
            {
                client.Authenticator = new RestSharp.Authenticators.JwtAuthenticator(UserService.GetToken());
            }
            catch (Exception)
            {
        
            }
        }
        public User GetUser(int id)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "users/" + id);
            IRestResponse<User> response = client.Get<User>(request);
            return ProcessResponse(response).Data;
        }

        public List<User> GetUsers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "users");
            IRestResponse<List<User>> response = client.Get<List<User>>(request);
            return ProcessResponse(response).Data;
        }

        public List<PublicAccount> GetAccounts()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts");
            IRestResponse<List<PublicAccount>> response = client.Get<List<PublicAccount>>(request);
            return ProcessResponse(response).Data;
        }

        public decimal GetBalance()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/" + UserService.GetUserId());
            IRestResponse<PrivateAccount> response = client.Get<PrivateAccount>(request);
            return ProcessResponse(response).Data.Balance;
        }

        public bool CreateTransfer(int id, Transfer requestTransfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/" + id + "/transfers");
            request.AddJsonBody(requestTransfer);
            IRestResponse response = client.Post(request);
            if (response.IsSuccessful)
            {
                return true;
            }
            else
            { 
                return false; 
            }
        }

        public List<Transfer> GetTransfers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/" + UserService.GetUserId() + "/transfers");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            return ProcessResponse(response).Data;
        }

        public Transfer GetSpecificTransfer(int transferID)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/" + UserService.GetUserId() + "/transfers/" + transferID);
            IRestResponse<Transfer> response = client.Get<Transfer>(request);
            if(response.Data.Amount == 0M)
                return null;
            return ProcessResponse(response).Data;
        }

        public List<Transfer> GetPendingTransfers()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/" + UserService.GetUserId() + "/transfers/pending");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            return ProcessResponse(response).Data;

        }

        public bool UpdateTransferStatus(int userId, Transfer transfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "accounts/" + userId + "/transfers/" + transfer.TransferID);
            request.AddJsonBody(transfer);
            IRestResponse response = client.Put(request);
            if (response.IsSuccessful)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private IRestResponse<T> ProcessResponse<T>(IRestResponse<T> responseIn)
        {
            if (responseIn.ResponseStatus != ResponseStatus.Completed)
            {
                throw new HttpRequestException("Error occurred - unable to reach server.");
            }
            else if (!responseIn.IsSuccessful)
            {
                throw new HttpRequestException("Error occurred - received non-success response: " + (int)responseIn.StatusCode);
            }
            return responseIn;
        }
    }
}
