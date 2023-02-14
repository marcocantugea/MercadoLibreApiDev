using MLApiConnector.Contractors;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MLApiConnector.Resources
{
    public class Users : IUsers
    {
        private RestClient _client;
        private string baseResources = "https://api.mercadolibre.com";
        private string resouces = "users";
        private int timeOut = 5000;

        public Users()
        {
            _client = new RestClient();

        }

        public Users(RestClientOptions clientOptions)
        {
            _client = new RestClient(clientOptions);
        }

        public Users(string baseResources, string resouces, int timeOut = 5000, string baseRedirectUrl = "https://localhost:7193")
        {
            _client = new RestClient();
            this.baseResources = baseResources;
            this.resouces = resouces;
            this.timeOut = timeOut;

        }

        public async Task<RestResponse> AddTestUser(string MLSiteId,string token)
        {
            _client.Options.MaxTimeout = timeOut;
            var uri = new Uri(baseResources + "/" + resouces+ "/test_user");
            var request = new RestRequest(uri, Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(JsonSerializer.Serialize(new { site_id=MLSiteId }));
            return await _client.ExecuteAsync(request);
        }

    }
}
