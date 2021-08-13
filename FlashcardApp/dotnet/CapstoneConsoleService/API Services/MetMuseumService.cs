using CapstoneClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.API_Services
{
    public class MetMuseumService
    {
        private readonly string API_URL = "https://collectionapi.metmuseum.org/public/collection/v1/";
        private readonly string API_Department = "search?q=all&departmentId=";
        private readonly string API_Images = "&hasImages=true&onDisplay=true";
        private readonly RestClient client = new RestClient();

        public MetMuseumDepartments GetMuseumDepartments()
        {
            RestRequest request = new RestRequest(API_URL + "departments");
            IRestResponse<MetMuseumDepartments> response = client.Get<MetMuseumDepartments>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {

                throw new Exception("Error in connecting to server or unsuccessful response");

            }
            else
            {
                return response.Data;
            }
        }

        public MetMuseumResult GetDepartmentObjects(int departmentID)
        {
            RestRequest request = new RestRequest(API_URL + API_Department + departmentID + API_Images);
            IRestResponse<MetMuseumResult> response = client.Get<MetMuseumResult>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {

                throw new Exception("Error in connecting to server or unsuccessful response");

            }
            else
            {
                return response.Data;
            }
        }

        public MetMuseumObject GetObjectDetail(int objectID)
        {
            MetMuseumObject mmaDetail = new MetMuseumObject();

            RestRequest request = new RestRequest(API_URL + "objects/" + objectID);
            IRestResponse<MetMuseumObject> response = client.Get<MetMuseumObject>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {

                Console.WriteLine((int)response.StatusCode);
                throw new Exception("Error in connecting to server or unsuccessful response");

            }
            else
            {
                mmaDetail = response.Data;
            }
            return mmaDetail;
        }
        
    }
}
