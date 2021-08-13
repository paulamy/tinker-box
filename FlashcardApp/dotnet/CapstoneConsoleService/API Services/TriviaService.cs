using System;
using RestSharp;
using System.Collections.Generic;
using System.Text;
using CapstoneClient.Models;

namespace CapstoneClient.API_Services
{
    public class TriviaService
    {
        private readonly string API_QUESTION_URL = "https://opentdb.com/api.php?";
        private readonly string API_CATEGORY_URL = "https://opentdb.com/api_category.php";
        private readonly string API_COUNT_URL = "https://opentdb.com/api_count.php?category=";
        private readonly RestClient client = new RestClient();

        public TriviaResult GetQuestions(int amount, int categoryID, string difficulty)
        {
            string amt = "amount="+amount;
            string cat = "&category=" + categoryID;
            string diff = "&difficulty=" + difficulty;
            RestRequest request = new RestRequest(API_QUESTION_URL+amt+cat+diff);
            IRestResponse<TriviaResult> response = client.Get<TriviaResult>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                throw new Exception("Error in connecting to server or unsuccessful response.");
            }
            else
            {
                return response.Data;
            }
        }

        public TriviaCategoryList GetCategoryList()
        {
            RestRequest request = new RestRequest(API_CATEGORY_URL);
            IRestResponse<TriviaCategoryList> response = client.Get<TriviaCategoryList>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                throw new Exception("Error in connecting to server or unsuccessful response.");
            }
            else
            {
                return response.Data;
            }
        }

        public TriviaQuestionCount GetCategoryQuestionCount(int categoryID)
        {
            RestRequest request = new RestRequest(API_COUNT_URL + categoryID);
            IRestResponse<TriviaQuestionCount> response = client.Get<TriviaQuestionCount>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                throw new Exception("Error in connecting to server or unsuccessful response.");
            }
            else
            {
                return response.Data;
            }
        }
    }
}
