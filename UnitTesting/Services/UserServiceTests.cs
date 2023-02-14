using Azure.Core;
using DataEF.Context;
using MLApiConnector.Contractors;
using MLApiContractors;
using MLApiServices;
using Moq;
using Newtonsoft.Json;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Fixtures;

namespace UnitTesting.Services
{
    public class UserServiceTests : IClassFixture<DatabaseFixture>
    {
        private MLApiDbContext _context;
        private Mock<IUsers> _resourceUsers;
        private Mock<IGlobalConfigurationServices> _globalConfigurationServices;
        private UserService _service;

        public UserServiceTests(DatabaseFixture fixture)
        {
            _context= fixture.GetContext();
            _resourceUsers = new Mock<IUsers>();
            _globalConfigurationServices= new Mock<IGlobalConfigurationServices>();
            _service = new UserService(_context, _resourceUsers.Object, _globalConfigurationServices.Object);
        }

        [Fact]
        public async void ShouldAddTestUser_Sucess() {

            var mlsite = _context.MLSites.Where(x => x.MLId == "MLA").FirstOrDefault();
            if (mlsite == null)
            {
                mlsite = new DataEF.Models.MLCatalogs.MLSites() { MLId = "MLA", DefaultCurrencyId = "MXP", Name = "FakeMlSite" };
                _context.MLSites.Add(mlsite);
                _context.SaveChanges();
            }

            string jsonContent = @"{""id"":120506781,""nickname"":""TEST0548"",""password"":""qatest328"",""site_status"":""active""}";

            _globalConfigurationServices
                .Setup(m => m.GetAuthToken())
                .ReturnsAsync(new MLApi.DTO.TokenInfoDTO()
                {
                    tokenAccess = "APP_USR-6239883022283152-020306-a53c3784fda0090153c49bf98b2a67a3-43051780",
                    expireIn = DateTime.Now.AddSeconds(20).ToString("yyyy-MM-dd hh:mm:ss")
                });

            _resourceUsers
                .Setup(m => m.AddTestUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = jsonContent,
                    IsSuccessStatusCode = true
                });

            var response = await _service.AddTestUser(mlsite.MLId);

            _globalConfigurationServices.Verify(m=>m.GetAuthToken(), Times.Once());
            _resourceUsers.Verify(m => m.AddTestUser(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
            var mluserCreate = _context.MLUsers.Where(m => m.MLId == response.MLid).FirstOrDefault();

            Assert.Equal(120506781, response.MLid);
            Assert.Equal(120506781, mluserCreate.MLId);

        }

        [Fact]
        public async void ShouldAddTestUser_Fail_MLSiteInvalid()
        {
            var exception=await Assert.ThrowsAsync<Exception>(async ()=> await _service.AddTestUser("fakeMlsite"));
            Assert.Equal("site not valid",exception.Message);
        }

        [Fact]
        public async void ShouldAddTestUser_Fail_NoResponse()
        {

            var mlsite = _context.MLSites.Where(x => x.MLId == "MLA").FirstOrDefault();
            if (mlsite == null)
            {
                mlsite = new DataEF.Models.MLCatalogs.MLSites() { MLId = "MLA", DefaultCurrencyId = "MXP", Name = "FakeMlSite" };
                _context.MLSites.Add(mlsite);
                _context.SaveChanges();
            }

            _globalConfigurationServices
                .Setup(m => m.GetAuthToken())
                .ReturnsAsync(new MLApi.DTO.TokenInfoDTO()
                {
                    tokenAccess = "APP_USR-6239883022283152-020306-a53c3784fda0090153c49bf98b2a67a3-43051780",
                    expireIn = DateTime.Now.AddSeconds(20).ToString("yyyy-MM-dd hh:mm:ss")
                });

            _resourceUsers
                .Setup(m => m.AddTestUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = null
                });

            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.AddTestUser(mlsite.MLId));
            Assert.Equal("error adding test user, response status code BadRequest", exception.Message);
        }

        [Fact]
        public async void ShouldAddTestUser_Fail_EmptyJson()
        {

            var mlsite = _context.MLSites.Where(x => x.MLId == "MLA").FirstOrDefault();
            if (mlsite == null)
            {
                mlsite = new DataEF.Models.MLCatalogs.MLSites() { MLId = "MLA", DefaultCurrencyId = "MXP", Name = "FakeMlSite" };
                _context.MLSites.Add(mlsite);
                _context.SaveChanges();
            }

            _globalConfigurationServices
                .Setup(m => m.GetAuthToken())
                .ReturnsAsync(new MLApi.DTO.TokenInfoDTO()
                {
                    tokenAccess = "APP_USR-6239883022283152-020306-a53c3784fda0090153c49bf98b2a67a3-43051780",
                    expireIn = DateTime.Now.AddSeconds(20).ToString("yyyy-MM-dd hh:mm:ss")
                });

            _resourceUsers
                .Setup(m => m.AddTestUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = "",
                    IsSuccessStatusCode = true
                });

            var exception = await Assert.ThrowsAnyAsync<Exception>(async () => await _service.AddTestUser(mlsite.MLId));
            Assert.NotEqual("", exception.Message);
        }

        [Fact]
        public async void ShouldAddTestUser_Fail_CanNotAddMore10Users()
        {

            var mlsite = _context.MLSites.Where(x => x.MLId == "MLA").FirstOrDefault();
            if (mlsite == null)
            {
                mlsite = new DataEF.Models.MLCatalogs.MLSites() { MLId = "MLA", DefaultCurrencyId = "MXP", Name = "FakeMlSite" };
                _context.MLSites.Add(mlsite);
                _context.SaveChanges();
            }

            var totalUsers= _context.MLUsers.Count(x=>x.IsTestUser);
            if (totalUsers < 10)
            {
                for (int i = totalUsers; i < 10; i++)
                {
                    _context.MLUsers.Add(new DataEF.Models.MLUsers.MLUser()
                    {
                        IsTestUser= true,
                        MLId=i+1,
                        NickName="test_user_"+i.ToString(),
                        Password="samepass",
                        site_status="active"
                    });
                }
            }
            _context.SaveChanges();

            var exception = await Assert.ThrowsAnyAsync<Exception>(async () => await _service.AddTestUser(mlsite.MLId));
            Assert.Equal("can not create more than 10 users", exception.Message);

            foreach (var row in _context.MLUsers) _context.MLUsers.Remove(row);
            _context.SaveChanges();
        }
    }
}
