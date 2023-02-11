using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiConnector.Contractors
{
    public interface IAuthResource
    {
        RestResponse GetToken(string clientId, string clientSecret, string code, string redirectUri = "");
        RestResponse GetRefreshToken(string clientId, string clientSecret, string refreshToken);
    }
}
