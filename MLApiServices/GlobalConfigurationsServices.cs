using DataEF.Context;
using Microsoft.AspNetCore.Mvc;
using MLApiContractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiServices
{
    public class GlobalConfigurationsServices : IGlobalConfigurationServices
    {
        private MLApiDbContext _context;

        public GlobalConfigurationsServices(MLApiDbContext context)
        {
            _context = context;
        }

        public async Task SaveClientAndSecretConfigurations(string clientId, string clientSecret)
        {
            try
            {
                if (string.IsNullOrEmpty(clientId)) throw new Exception("client id is empty");
                if (string.IsNullOrEmpty(clientSecret)) throw new Exception("client secret is empty");

                var clientIdRecord = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_ID").FirstOrDefault();
                var clientSecretRecord = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_SECRET").FirstOrDefault();
                
                if (clientIdRecord == null) throw new Exception("global variable not found");
                if (clientSecretRecord == null) throw new Exception("global variable not found");

                clientIdRecord.Value = clientId;
                clientIdRecord.updated = DateTime.Now;
                clientSecretRecord.Value= clientSecret;
                clientSecretRecord.updated = DateTime.Now;

                _context.GlobalConfigurations.Update(clientIdRecord);
                _context.GlobalConfigurations.Update(clientSecretRecord);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveCode(string tcode)
        {
            try
            {
                if (String.IsNullOrEmpty(tcode)) throw new Exception("invalid code");

                var mlcode = _context.GlobalConfigurations.Where(gc => gc.Name == "ML_CODE").FirstOrDefault();

                if (mlcode == null) throw new Exception("global variable not found");

                mlcode.Value = tcode;
                mlcode.updated = DateTime.Now; 
                _context.GlobalConfigurations.Update(mlcode);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
