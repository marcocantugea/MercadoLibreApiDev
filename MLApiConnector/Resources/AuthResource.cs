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
        private string baseRedirectUrl;

        public AuthResource()
        {
            _client = new RestClient();
            baseRedirectUrl = "https://localhost:7193";
        }

        public AuthResource(RestClientOptions clientOptions)
        {
            _client = new RestClient(clientOptions);
        }

        public AuthResource(string baseResources, string resouces, int timeOut=5000,string baseRedirectUrl = "https://localhost:7193")
        {
            _client = new RestClient();
            this.baseResources = baseResources;
            this.resouces = resouces;
            this.timeOut= timeOut;
            this.baseRedirectUrl = baseRedirectUrl;

        }

        public RestResponse GetToken(string clientId, string clientSecret, string code, string redirectUri="") {
            _client.Options.MaxTimeout = timeOut;
            var uri= new Uri(baseResources+"/"+resouces);
            var request = new RestRequest(uri,Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", (redirectUri.Equals(string.Empty) ? baseRedirectUrl : redirectUri ));
            RestResponse response = _client.Execute(request);
            return response;
        }

        public RestResponse GetRefreshToken(string clientId, string clientSecret, string refreshToken)
        {
            _client.Options.MaxTimeout = timeOut;
            var uri = new Uri(baseResources + "/" + resouces);
            var request = new RestRequest(uri, Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("refresh_token", refreshToken);
            RestResponse response = _client.Execute(request);
            return response;
        }
    }
}
