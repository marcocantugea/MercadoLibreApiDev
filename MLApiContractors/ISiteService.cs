using DataEF.Models.MLCatalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiContractors
{
    public interface ISiteService
    {
        Task SaveSites(IEnumerable<MLSites> sites);
        Task SaveSite(string MLId, string name, string defaultCurrencyId);
        Task LoadSitesToDb();
    }
}
