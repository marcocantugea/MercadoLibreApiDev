using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEF.Models.Global
{
    public class GlobalConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool active { get; set; }
        public DateTime created { get; set; }
        public DateTime? updated { get; set; }

    }
}
