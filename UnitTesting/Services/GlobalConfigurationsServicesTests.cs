using DataEF.Context;
using MLApiServices;
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

        public GlobalConfigurationsServicesTests(DatabaseFixture fixture) { 
            _context=fixture.GetContext();
            _service = new GlobalConfigurationsServices(_context);
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

    }
}
