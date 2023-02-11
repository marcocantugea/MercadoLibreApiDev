using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApi.DTO
{
    public class TokenInfoDTO
    {
        public string tokenAccess { get; set; }
        public string expireIn { get; set; }
    }
}
