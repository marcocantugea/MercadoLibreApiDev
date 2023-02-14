using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Models.MLUsers
{
    public class MLUser
    {
        public int Id { get; set; }
        public int MLId { get; set; }
        public string NickName { get;set;}
        public string Password { get; set;}
        public string site_status { get; set;}
        public bool IsTestUser { get; set;}
    }
}
