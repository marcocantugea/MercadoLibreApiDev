using DataEF.Context;
using DataEF.Models.MLCatalogs;
using MLApiConnector.Contractors;
using MLApiConnector.SerializedResponses;
using MLApiContractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MLApiServices
{
    public class SiteService : ISiteService
    {
        private MLApiDbContext _context;
        private ISites _resouceSites;

        public SiteService(MLApiDbContext context,ISites resourceSites)
        {
            _context= context;
            _resouceSites= resourceSites;
        }

        public async Task SaveSites(IEnumerable<MLSites> sites)
        {
            try
            {
                await _context.AddRangeAsync(sites);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task SaveSite(string MLId, string name, string defaultCurrencyId)
        {
            try
            {
                if (string.IsNullOrEmpty(MLId) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(defaultCurrencyId))
                    throw new Exception("null o empty string on parameters");

                var site = new MLSites()
                {
                    MLId = MLId,
                    Name = name,
                    DefaultCurrencyId = defaultCurrencyId
                };

                _context.MLSites.Add(site);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task LoadSitesToDb()
        {
            try
            {
                var response = await _resouceSites.GetSites();
                if(response.IsSuccessStatusCode)
                {
                    var jsonResponse= JsonSerializer.Deserialize<List<SiteResponse>>(response.Content);
                    List<MLSites> sites = new List<MLSites>();
                    foreach (var item in jsonResponse)
                    {
                        sites.Add(new MLSites()
                        {
                            MLId = item.id,
                            Name = item.name,
                            DefaultCurrencyId=item.default_currency_id
                        });
                    }

                    int totalOnDb = _context.MLSites.Count();
                    if(totalOnDb < jsonResponse.Count) SaveSites(sites);

                }
                else
                {
                    throw new Exception($"can not load the sites, response code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
