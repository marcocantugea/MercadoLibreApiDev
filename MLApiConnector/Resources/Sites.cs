using MLApiConnector.Contractors;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiConnector.Resources
{
    public class Sites : ISites
    {
        private RestClient _client;
        private string baseResources = "https://api.mercadolibre.com";
        private string resouces = "/sites";
        private int timeOut = 5000;
        

        public Sites()
        {
            _client = new RestClient();
        
        }

        public Sites(RestClientOptions clientOptions)
        {
            _client = new RestClient(clientOptions);
        }

        public Sites(string baseResources, string resouces, int timeOut = 5000, string baseRedirectUrl = "https://localhost:7193")
        {
            _client = new RestClient();
            this.baseResources = baseResources;
            this.resouces = resouces;
            this.timeOut = timeOut;

        }

        public async Task<RestResponse> GetSites()
        {
            _client.Options.MaxTimeout = timeOut;
            var uri = new Uri(baseResources + "/" + resouces);
            var request = new RestRequest(uri, Method.Get);
            request.AddHeader("Accept", "application/json");
            return await _client.ExecuteAsync(request);
        }
    }
}
