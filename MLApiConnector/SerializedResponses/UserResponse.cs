using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiConnector.SerializedResponses
{
    public class UserResponse
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
        public string site_status { get; set; }
    }
}
