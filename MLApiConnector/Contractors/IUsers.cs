using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiConnector.Contractors
{
    public interface IUsers
    {

        Task<RestResponse> AddTestUser(string MLSiteId, string token);
    }
}
