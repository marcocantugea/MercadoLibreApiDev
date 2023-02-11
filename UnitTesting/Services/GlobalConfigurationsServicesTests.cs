using DataEF.Context;
using DataEF.Models.Global;
using MLApiConnector.Contractors;
using MLApiConnector.SerializedResponses;
using MLApiServices;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Fixtures;

namespace UnitTesting.Services
{
    public class GlobalConfigurationsServicesTests : IClassFixture<DatabaseFixture>
    {
        private MLApiDbContext _context;
        private GlobalConfigurationsServices _service;
        private Mock<IAuthResource> _authResource;

        public GlobalConfigurationsServicesTests(DatabaseFixture fixture) { 
            _context=fixture.GetContext();
            _authResource=new Mock<IAuthResource>();
            _service = new GlobalConfigurationsServices(_context,_authResource.Object);
        }

        [Fact]
        public async void ShouldSaveClientAndSecretConfigurations_Success()
        {
            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");

            var clientId= _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_ID").FirstOrDefault();
            var clientSecret = _context.GlobalConfigurations.FirstOrDefault(gc => gc.Name == "CLIENT_SECRET");


            Assert.Equal("test", clientId.Value);
            Assert.Equal("testclientsecret", clientSecret.Value);
        }

        [Fact]
        public async void ShouldSaveClientAndSecretConfigurations_Fail_ClientIDEmpty()
        {
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveClientAndSecretConfigurations(String.Empty, "clientsecret"));
            Assert.Equal("client id is empty", exception.Message);
        }

        [Fact]
        public async void ShouldSaveClientAndSecretConfigurations_Fail_ClientSecretEmpty()
        {
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveClientAndSecretConfigurations("test", string.Empty));
            Assert.Equal("client secret is empty", exception.Message);
        }

        [Fact]
        public async void ShouldSaveClientAndSecretConfigurations_Fail_NoClientIDVariableFound()
        {

            var clientId = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_ID").FirstOrDefault();
            clientId.Name = "CLIENT_ID2";
            _context.Update(clientId);
            _context.SaveChanges();

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveClientAndSecretConfigurations("test", "clientsecret"));
            Assert.Equal("global variable not found", exception.Message);


            clientId.Name = "CLIENT_ID";
            _context.Update(clientId);
            _context.SaveChanges();
        }

        [Fact]
        public async void ShouldSaveClientAndSecretConfigurations_Fail_NoSecretVariableFound()
        {

            var clientId = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_SECRET").FirstOrDefault();
            clientId.Name = "CLIENT_SECRET2";
            _context.Update(clientId);
            _context.SaveChanges();

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveClientAndSecretConfigurations("test", "clientsecret"));
            Assert.Equal("global variable not found", exception.Message);


            clientId.Name = "CLIENT_SECRET";
            _context.Update(clientId);
            _context.SaveChanges();
        }

        [Fact]
        public async void ShouldSaveTCode_Sucess()
        {
            await _service.SaveCode("test");
            var mlcode= _context.GlobalConfigurations.Where(gc => gc.Name == "ML_CODE").FirstOrDefault();
            Assert.Equal("test", mlcode.Value);
        }

        [Fact]
        public async void ShouldSaveTCode_Fail_EmptyTcode()
        {
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveCode(string.Empty));
            Assert.Equal("invalid code", exception.Message);
        }

        [Fact]
        public async void ShouldSaveTCode_Fail_NoGlobalVariableFound()
        {
            var mlcode = _context.GlobalConfigurations.Where(gc => gc.Name == "ML_CODE").FirstOrDefault();
            mlcode.Name = "ML_CODE2";
            _context.Update(mlcode);
            _context.SaveChanges();

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveCode("test"));
            Assert.Equal("global variable not found", exception.Message);

            mlcode.Name = "ML_CODE";
            _context.Update(mlcode);
            _context.SaveChanges();
        }

        [Fact]
        public async void ShouldGetAuthLink_Success()
        {
            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");
            var clientid=_context.GlobalConfigurations.Where(gc=>gc.Name== "CLIENT_ID").Select(gc=>gc.Value).FirstOrDefault();
            var link=await _service.GetAuthLink("test");
            Assert.Equal($"https://auth.mercadolibre.com.mx/authorization?response_type=code&client_id={clientid}&redirect_uri=test",link);
        }


        [Fact]
        public async void ShouldGetAuthLink_Fail_ClientIdNotFound()
        {
            var clientidObj = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_ID").FirstOrDefault();
            clientidObj.Value = string.Empty;
            _context.Update(clientidObj);
            _context.SaveChanges();

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.GetAuthLink("test"));
            Assert.Equal("client id not found", exception.Message);
        }

        [Fact]
        public async void ShouldSaveTokenInfo_Succes()
        {
            await _service.SaveTokenInfo("token", 1000, 12345, "refreshtoken");
            var itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();
            var tokenSaved= itemsConfig.Where(i=>i.Name== "ACCESS_TOKEN").Select(i=>i.Value).FirstOrDefault();
            var expireIntMiliseconds = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").Select(i => i.Value).FirstOrDefault();
            var userid = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").Select(i => i.Value).FirstOrDefault();
            var refreshtoken = itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").Select(i => i.Value).FirstOrDefault();
            var expireDate = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").Select(i => i.Value).FirstOrDefault();

            Assert.Equal("token", tokenSaved);
            Assert.Equal(1000, int.Parse(expireIntMiliseconds));
            Assert.Equal(12345, int.Parse(userid));
            Assert.Equal("refreshtoken", refreshtoken);
            Assert.True(DateTime.TryParse(expireDate,out DateTime result));
        }

        [Fact]
        public async void ShouldSaveTokenInfo_Fail_TokenEmpty()
        {
            var exception =await Assert.ThrowsAsync<Exception>(async ()=> await _service.SaveTokenInfo(String.Empty, 1000, 12345, "refreshtoken"));
            Assert.Equal("access token or refresh token is empty", exception.Message);
            
        }

        [Fact]
        public async void ShouldSaveTokenInfo_Fail_RefreshTokenEmpty()
        {
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveTokenInfo("test", 1000, 12345, string.Empty));
            Assert.Equal("access token or refresh token is empty", exception.Message);

        }

        [Fact]
        public async void ShouldSaveTokenInfo_Fail_ExpireEmpty()
        {
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveTokenInfo("test", 0, 12345, "tokenrefresh"));
            Assert.Equal("expire time or user id is invalid", exception.Message);
        }

        [Fact]
        public async void ShouldSaveTokenInfo_Fail_UserIdEmpty()
        {
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.SaveTokenInfo("test", 1000, 0, "tokenrefresh"));
            Assert.Equal("expire time or user id is invalid", exception.Message);
        }

        [Fact]
        public async void ShouldGetAuthToken_Success_NullToken()
        {
            //actualiza todos los campos del token como null
            foreach(var row in _context.GlobalConfigurations.Where(gc=>gc.Name.Contains("TOKEN")).ToList())
            {
                row.Value = null;
                _context.Update(row);
            }

            _context.SaveChanges();

            //configuramos el string de respuesta
            string jsonContent = @"{""access_token"": ""APP_USR-2780064669442467-021111-bb9bd6f082c902dbd325360cb90b56ed-43051780"",""token_type"": ""Bearer"",""expires_in"":21600,""scope"":""offline_access read write"",""user_id"":43051780,""refresh_token"":""TG-63e72c5e112e1600011584d0-43051780""}";
            var contentJson = JsonConvert.DeserializeObject<TokenResponse>(jsonContent);

            //configuramos el mock
            _authResource
                .Setup(m => m.GetToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new RestSharp.RestResponse() {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = jsonContent,
                    IsSuccessStatusCode= true
             });

            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");
            await _service.SaveCode("code");

            var response = await _service.GetAuthToken();

            var itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();
            var tokenSaved = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").Select(i => i.Value).FirstOrDefault();
            var expireIntMiliseconds = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").Select(i => i.Value).FirstOrDefault();
            var userid = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").Select(i => i.Value).FirstOrDefault();
            var refreshtoken = itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").Select(i => i.Value).FirstOrDefault();
            var expireDate = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").Select(i => i.Value).FirstOrDefault();

            _authResource.Verify(m => m.GetToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),Times.AtLeastOnce);
            Assert.Equal(contentJson.access_token, tokenSaved);
            Assert.Equal(contentJson.expires_in, int.Parse(expireIntMiliseconds));
            Assert.Equal(contentJson.user_id, int.Parse(userid));
            Assert.Equal(contentJson.refresh_token, refreshtoken);
            Assert.True(DateTime.TryParse(expireDate, out DateTime result));
            Assert.Equal(contentJson.access_token, response.tokenAccess);

        }

        [Fact]
        public async void ShouldGetAuthToken_Fail_ErrorResponse()
        {
            //actualiza todos los campos del token como null
            foreach (var row in _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList())
            {
                row.Value = null;
                _context.Update(row);
            }

            _context.SaveChanges();

            //configuramos el mock
            _authResource
                .Setup(m => m.GetToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    IsSuccessStatusCode = false
                });

            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");
            await _service.SaveCode("code");

            var exception=await Assert.ThrowsAsync<Exception>(async () => await _service.GetAuthToken());

            _authResource.Verify(m => m.GetToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            Assert.Equal("error getting token status response InternalServerError", exception.Message);
        }

        [Fact]
        public async void ShouldGetAuthToken_Success_NullExpireDate()
        {

            //configuramos el string de respuesta
            string jsonContent = @"{""access_token"": ""APP_USR-2780064669442467-021111-bb9bd6f082c902dbd325360cb90b56ed-43051780"",""token_type"": ""Bearer"",""expires_in"":21600,""scope"":""offline_access read write"",""user_id"":43051780,""refresh_token"":""TG-63e72c5e112e1600011584d0-43051780""}";
            var contentJson = JsonConvert.DeserializeObject<TokenResponse>(jsonContent);

            var itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();
            var token = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault();
            var expireIntMiliseconds = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault();
            var userid = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault();
            var refreshtoken = itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault();
            var expireDate = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault();

            token.Value = contentJson.access_token;
            expireIntMiliseconds.Value=contentJson.expires_in.ToString();
            userid.Value = contentJson.user_id.ToString();
            refreshtoken.Value=contentJson.refresh_token;
            expireDate.Value=null;

            _context.UpdateRange(new List<GlobalConfiguration>(){ token,expireIntMiliseconds,userid,refreshtoken,expireDate });
            _context.SaveChanges();

            //configuramos el mock
            _authResource
                .Setup(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = jsonContent,
                    IsSuccessStatusCode = true
                });

            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");
            await _service.SaveCode("code");

            var response = await _service.GetAuthToken();

            itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();

            _authResource.Verify(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            Assert.Equal(contentJson.access_token, itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault().Value);
            Assert.Equal(contentJson.expires_in,int.Parse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault().Value));
            Assert.Equal(contentJson.user_id, int.Parse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault().Value));
            Assert.Equal(contentJson.refresh_token, itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault().Value);
            Assert.True(DateTime.TryParse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault().Value, out DateTime result));
            Assert.Equal(contentJson.access_token, response.tokenAccess);

        }

        [Fact]
        public async void ShouldGetAuthToken_Success_ExpireDateNotDate()
        {

            //configuramos el string de respuesta
            string jsonContent = @"{""access_token"": ""APP_USR-2780064669442467-021111-bb9bd6f082c902dbd325360cb90b56ed-43051780"",""token_type"": ""Bearer"",""expires_in"":21600,""scope"":""offline_access read write"",""user_id"":43051780,""refresh_token"":""TG-63e72c5e112e1600011584d0-43051780""}";
            var contentJson = JsonConvert.DeserializeObject<TokenResponse>(jsonContent);

            var itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();
            var token = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault();
            var expireIntMiliseconds = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault();
            var userid = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault();
            var refreshtoken = itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault();
            var expireDate = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault();

            token.Value = contentJson.access_token;
            expireIntMiliseconds.Value = contentJson.expires_in.ToString();
            userid.Value = contentJson.user_id.ToString();
            refreshtoken.Value = contentJson.refresh_token;
            expireDate.Value = "this is a date";

            _context.UpdateRange(new List<GlobalConfiguration>() { token, expireIntMiliseconds, userid, refreshtoken, expireDate });
            _context.SaveChanges();

            //configuramos el mock
            _authResource
                .Setup(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = jsonContent,
                    IsSuccessStatusCode = true
                });

            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");
            await _service.SaveCode("code");

            var response = await _service.GetAuthToken();

            itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();

            _authResource.Verify(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            Assert.Equal(contentJson.access_token, itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault().Value);
            Assert.Equal(contentJson.expires_in, int.Parse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault().Value));
            Assert.Equal(contentJson.user_id, int.Parse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault().Value));
            Assert.Equal(contentJson.refresh_token, itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault().Value);
            Assert.True(DateTime.TryParse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault().Value, out DateTime result));
            Assert.Equal(contentJson.access_token, response.tokenAccess);

        }

        [Fact]
        public async void ShouldGetAuthToken_Success_VoidExpiredDate()
        {

            //configuramos el string de respuesta
            string jsonContent = @"{""access_token"": ""APP_USR-2780064669442467-021111-bb9bd6f082c902dbd325360cb90b56ed-43051780"",""token_type"": ""Bearer"",""expires_in"":21600,""scope"":""offline_access read write"",""user_id"":43051780,""refresh_token"":""TG-63e72c5e112e1600011584d0-43051780""}";
            var contentJson = JsonConvert.DeserializeObject<TokenResponse>(jsonContent);

            var itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();
            var token = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault();
            var expireIntMiliseconds = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault();
            var userid = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault();
            var refreshtoken = itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault();
            var expireDate = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault();

            token.Value = contentJson.access_token;
            expireIntMiliseconds.Value = contentJson.expires_in.ToString();
            userid.Value = contentJson.user_id.ToString();
            refreshtoken.Value = contentJson.refresh_token;
            expireDate.Value = DateTime.Now.AddMinutes(-20).ToString("yyyy-MM-dd hh:mm:ss");

            _context.UpdateRange(new List<GlobalConfiguration>() { token, expireIntMiliseconds, userid, refreshtoken, expireDate });
            _context.SaveChanges();

            //configuramos el mock
            _authResource
                .Setup(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = jsonContent,
                    IsSuccessStatusCode = true
                });

            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");
            await _service.SaveCode("code");

            var response = await _service.GetAuthToken();

            itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();

            _authResource.Verify(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            Assert.Equal(contentJson.access_token, itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault().Value);
            Assert.Equal(contentJson.expires_in, int.Parse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault().Value));
            Assert.Equal(contentJson.user_id, int.Parse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault().Value));
            Assert.Equal(contentJson.refresh_token, itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault().Value);
            Assert.True(DateTime.TryParse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault().Value, out DateTime result));
            Assert.Equal(contentJson.access_token, response.tokenAccess);

        }

        [Fact]
        public async void ShouldGetAuthToken_Success_TokenFromDb()
        {

            //configuramos el string de respuesta
            string jsonContent = @"{""access_token"": ""APP_USR-2780064669442467-021111-bb9bd6f082c902dbd325360cb90b56ed-43051780"",""token_type"": ""Bearer"",""expires_in"":21600,""scope"":""offline_access read write"",""user_id"":43051780,""refresh_token"":""TG-63e72c5e112e1600011584d0-43051780""}";
            var contentJson = JsonConvert.DeserializeObject<TokenResponse>(jsonContent);

            var itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();
            var token = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault();
            var expireIntMiliseconds = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault();
            var userid = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault();
            var refreshtoken = itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault();
            var expireDate = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault();

            token.Value = contentJson.access_token;
            expireIntMiliseconds.Value = contentJson.expires_in.ToString();
            userid.Value = contentJson.user_id.ToString();
            refreshtoken.Value = contentJson.refresh_token;
            expireDate.Value = DateTime.Now.AddMinutes(contentJson.expires_in).ToString("yyyy-MM-dd hh:mm:ss");

            _context.UpdateRange(new List<GlobalConfiguration>() { token, expireIntMiliseconds, userid, refreshtoken, expireDate });
            _context.SaveChanges();

            //configuramos el mock
            _authResource
                .Setup(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = jsonContent,
                    IsSuccessStatusCode = true
                });

            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");
            await _service.SaveCode("code");

            var response = await _service.GetAuthToken();

            itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();

            Assert.Equal(contentJson.access_token, itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault().Value);
            Assert.Equal(contentJson.expires_in, int.Parse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault().Value));
            Assert.Equal(contentJson.user_id, int.Parse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault().Value));
            Assert.Equal(contentJson.refresh_token, itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault().Value);
            Assert.True(DateTime.TryParse(itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault().Value, out DateTime result));
            Assert.Equal(contentJson.access_token, response.tokenAccess);

        }


        [Fact]
        public async void ShouldRefreshToken_Success()
        {

            //configuramos el string de respuesta
            string jsonContent = @"{""access_token"": ""APP_USR-2780064669442467-021111-bb9bd6f082c902dbd325360cb90b56ed-43051780"",""token_type"": ""Bearer"",""expires_in"":21600,""scope"":""offline_access read write"",""user_id"":43051780,""refresh_token"":""TG-63e72c5e112e1600011584d0-43051780""}";
            var contentJson = JsonConvert.DeserializeObject<TokenResponse>(jsonContent);

            //configuramos el refresh token en la base de datos
            var refreshtoken_actual= _context.GlobalConfigurations.Where(gc => gc.Name.Contains("REFRESH_TOKEN")).FirstOrDefault();
            refreshtoken_actual.Value = contentJson.refresh_token;
            _context.Update(refreshtoken_actual);
            _context.SaveChanges();

            //configuramos el mock
            _authResource
                .Setup(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = jsonContent,
                    IsSuccessStatusCode = true
                });

            await _service.SaveClientAndSecretConfigurations("test", "testclientsecret");
            await _service.SaveCode("code");

            var response = await _service.RefreshToken();

            var itemsConfig = _context.GlobalConfigurations.Where(gc => gc.Name.Contains("TOKEN")).ToList();
            var tokenSaved = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").Select(i => i.Value).FirstOrDefault();
            var expireIntMiliseconds = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").Select(i => i.Value).FirstOrDefault();
            var userid = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").Select(i => i.Value).FirstOrDefault();
            var refreshtoken = itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").Select(i => i.Value).FirstOrDefault();
            var expireDate = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").Select(i => i.Value).FirstOrDefault();

            _authResource.Verify(m => m.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            Assert.Equal(contentJson.access_token, tokenSaved);
            Assert.Equal(contentJson.expires_in, int.Parse(expireIntMiliseconds));
            Assert.Equal(contentJson.user_id, int.Parse(userid));
            Assert.Equal(contentJson.refresh_token, refreshtoken);
            Assert.True(DateTime.TryParse(expireDate, out DateTime result));
            Assert.Equal(contentJson.access_token, response.tokenAccess);

        }
    }
}
