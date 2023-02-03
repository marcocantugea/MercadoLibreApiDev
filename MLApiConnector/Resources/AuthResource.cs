using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiConnector.Resources
{
    public class AuthResource
    {
        private RestClient _client;
        private string baseResources = "https://api.mercadolibre.com";
        private string resouces = "oauth/token";
        private int timeOut = 5000;

        public AuthResource()
        {
            _client = new RestClient();
        }

        public AuthResource(RestClientOptions clientOptions)
        {
            _client = new RestClient(clientOptions);
        }

        public AuthResource(RestClient client,string baseResources, string resouces, int timeOut=5000)
        {
            _client = new RestClient();
            this.baseResources = baseResources;
            this.resouces = resouces;
            this.timeOut= timeOut;

        }

        public RestResponse GetToken(string clientId, string clientSecret, string code, string redirectUri) {
            _client.Options.MaxTimeout = timeOut;
            var uri= new Uri(baseResources+"/"+resouces);
            var request = new RestRequest(uri,Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", "https://localhost:7192");
            RestResponse response = _client.Execute(request);
            return response;
        }
    }
}
