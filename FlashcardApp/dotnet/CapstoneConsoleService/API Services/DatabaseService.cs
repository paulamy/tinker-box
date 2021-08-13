using CapstoneClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace CapstoneClient.API_Services
{
    public class DatabaseService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();
        const int adminID = 2;

        public Deck GetDeck(string name)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "decks/" + name);
            IRestResponse<Deck> response = client.Get<Deck>(request);
            return ProcessResponse(response).Data;
        }

        public DBCategory GetCategoryByName(string name)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "categories/name/" + name);
            IRestResponse<DBCategory> response = client.Get<DBCategory>(request);
            return ProcessResponse(response).Data;
        }

        public List<DBCategory> GetCategories()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "categories");
            IRestResponse<List<DBCategory>> response = client.Get<List<DBCategory>>(request);
            return ProcessResponse(response).Data;
        }

        public bool CreateDeck(Deck newDeck)
        {
            
            RestRequest request = new RestRequest(API_BASE_URL + "decks/user/" + adminID);
            request.AddJsonBody(newDeck);
            IRestResponse response = client.Post(request);
            if (response.IsSuccessful)
            {
                
                return true;
            }
            else
            {
                return false ;
            }
        }

        public bool CreateCard(int deckID, Card card)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(API_BASE_URL + "cards/user/" + adminID + "/deck/" + deckID);
            request.AddJsonBody(card);
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
