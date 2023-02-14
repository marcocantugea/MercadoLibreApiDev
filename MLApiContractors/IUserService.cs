using MLApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiContractors
{
    public interface IUserService
    {

        Task<MLUserDTO> AddTestUser(string MLSite);
    }
}
