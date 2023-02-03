using MLApiConnector.Resources;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RichardSzalay.MockHttp;

namespace UnitTesting.MLApiConnector
{
    public class AuthResourceTests
    {
        private MockHttpMessageHandler _httpMessageHandlerMock;
        private AuthResource _authResource;

        public AuthResourceTests()
        {
            
            _httpMessageHandlerMock = new MockHttpMessageHandler();

            _httpMessageHandlerMock
            .When("https://api.mercadolibre.com/oauth/token")
            .Respond("application/json", "{\"access_token\":\"APP_USR-6239883022283152-013022-2020b152d68d5812cfedd66b9ba9860d-43051780\",\"token_type\":\"Bearer\",\"expires_in\":21600,\"scope\":\"offline_access read write\",\"user_id\":43051780,\"refresh_token\":\"TG-63d8780226ceb3000192cf56-43051780\"}");

            _authResource = new AuthResource(new RestClientOptions { ConfigureMessageHandler= _=> _httpMessageHandlerMock });
        }

        [Fact]
        public void ShouldGetToken_Success()
        {
            var response = _authResource.GetToken("clientid", "clientSecret", "code", "localhost");

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
