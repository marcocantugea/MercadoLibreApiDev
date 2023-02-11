using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MLApiContractors
{
    public interface IGlobalConfigurationServices
    {
        Task SaveClientAndSecretConfigurations(string clientId, string clientSecret);
        Task SaveCode(string tcode);
        Task<string> GetAuthLink(string redirectUrl);
    }
}
