using DataEF.Context;
using DataEF.Models.MLCatalogs;
using MLApiConnector.Contractors;
using MLApiServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Fixtures;

namespace UnitTesting.Services
{
    public class SiteServiceTests : IClassFixture<DatabaseFixture>
    {
        private DatabaseFixture _fixture;
        private MLApiDbContext _context;
        private Mock<ISites> _sites;
        private SiteService _service;

        public SiteServiceTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _context = fixture.GetContext();
            _sites = new Mock<ISites>();
            _service = new SiteService(_context,_sites.Object);
        }

        [Fact]
        public async void ShouldSaveSite_Success()
        {
            await _service.SaveSite("mlid123", "name", "defaultCurrency");
            var site = _context.MLSites.Where(x => x.MLId == "mlid123").FirstOrDefault();
            Assert.NotNull(site);
            Assert.Equal("name", site.Name);
            Assert.Equal("defaultCurrency", site.DefaultCurrencyId);
        }

        [Theory]
        [InlineData("", "name1", "USD")]
        [InlineData("mlid143", "", "USD" )]
        [InlineData("mlid143", "name1", "")]
        public async void ShouldSaveSite_Fail_emptystrings(string mlid, string name, string defaultCurrency)
        {
            var exception=await Assert.ThrowsAnyAsync<Exception>(async ()=> await _service.SaveSite(mlid, name, defaultCurrency));
            Assert.Equal("null o empty string on parameters", exception.Message);
        }

        [Fact]
        public async void ShouldSaveSites_Succes()
        {
            var sites = new List<MLSites>()
            {
                new MLSites()
                {
                    MLId="MLM",
                    DefaultCurrencyId="MXN",
                    Name="Mexico",
                },
                new MLSites()
                {
                    MLId="MCO",
                    DefaultCurrencyId="COP",
                    Name="Colombia",
                },
                new MLSites()
                {
                    MLId="BOB",
                    DefaultCurrencyId="MBO",
                    Name="Bolivia",
                },
            };

            await _service.SaveSites(sites);
            var totalSites= _context.MLSites.Count();

            Assert.InRange(totalSites, 1, 100);
        }

        [Fact]
        public async void ShouldLoadSitesToDb_Success()
        {
            string MockResponse = @"[{""default_currency_id"": ""CUP"",""id"": ""MCU"",""name"": ""Cuba""},{""default_currency_id"": ""GTQ"",""id"": ""MGT"",""name"": ""Guatemala""},{""default_currency_id"": ""PAB"",""id"": ""MPA"",""name"": ""Panamá""},{""default_currency_id"": ""USD"",""id"": ""MEC"",""name"": ""Ecuador""},{""default_currency_id"": ""PEN"",""id"": ""MPE"",""name"": ""Perú""}]";

            _sites
                .Setup(x=>x.GetSites())
                .ReturnsAsync(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = MockResponse,
                    IsSuccessStatusCode = true,
                    ContentType= "application/json",
                    
                });

            await _service.LoadSitesToDb();

            _sites.Verify(x => x.GetSites(),Times.AtLeastOnce);
            int totalOfRecords= _context.MLSites.Count();
            var ecuadorSite = _context.MLSites.Where(x => x.MLId == "MPE").FirstOrDefault();

            Assert.InRange(totalOfRecords, 1, 100);
            Assert.NotNull(ecuadorSite);
        }

        [Fact]
        public async void ShouldLoadSitesToDb_Fail_BadRequestResponse()
        {

            _sites
                .Setup(x => x.GetSites())
                .ReturnsAsync(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccessStatusCode = false,
                });

            var exception = await Assert.ThrowsAsync<Exception>(async()=> await _service.LoadSitesToDb());
            _sites.Verify(x => x.GetSites(), Times.AtLeastOnce);
            Assert.Equal("can not load the sites, response code BadRequest",exception.Message);
        }

        [Fact]
        public async void ShouldLoadSitesToDb_Fail_NoContentInResponse()
        {

            _sites
                .Setup(x => x.GetSites())
                .ReturnsAsync(new RestSharp.RestResponse()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = "",
                    IsSuccessStatusCode = true,
                    ContentType = "application/json",
                });

            var exception = await Assert.ThrowsAnyAsync<Exception>(async () => await _service.LoadSitesToDb());
            _sites.Verify(x => x.GetSites(), Times.AtLeastOnce);
            Assert.NotEqual("", exception.Message);
        }
    }
}
