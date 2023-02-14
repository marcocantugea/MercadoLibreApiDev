using DataEF.Context;
using DataEF.Models.MLUsers;
using MLApi.DTO;
using MLApiConnector.Contractors;
using MLApiConnector.Resources;
using MLApiConnector.SerializedResponses;
using MLApiContractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MLApiServices
{
    public class UserService : IUserService
    {
        private MLApiDbContext _context;
        private IUsers _resourceUsers;
        private IGlobalConfigurationServices _globalConfigurationServices;

        public UserService(MLApiDbContext context,IUsers resourceUsers, IGlobalConfigurationServices globalConfigurationServices)
        {
            _context = context;
            _resourceUsers = resourceUsers;
            _globalConfigurationServices = globalConfigurationServices;
        }

        public async Task<MLUserDTO> AddTestUser(string MLSite)
        {
            try
            {
                //revisamos si el MLSite existe
                var mlsite= _context.MLSites.Where(x=>x.MLId==MLSite).FirstOrDefault();
                // ML solo deja crear hasta 10 usuarios hay que limitar los usuarios
                var totalUsuariosTest= _context.MLUsers.Count(m=>m.IsTestUser);

                if (mlsite == null) throw new Exception("site not valid");
                if(totalUsuariosTest==10) throw new Exception("can not create more than 10 users");

                //enviamos la peticion para agregar el ususario
                var tokenInfo = await _globalConfigurationServices.GetAuthToken();
                var response = await _resourceUsers.AddTestUser(mlsite.MLId,tokenInfo.tokenAccess);
                if(response.IsSuccessStatusCode)
                {
                    // recibimos respuest y guardamos en base de datos.
                    var parsedContent =JsonSerializer.Deserialize<UserResponse>(response.Content);
                    var newTestUser = new MLUser()
                    {
                        MLId = parsedContent.id,
                        NickName = parsedContent.nickname,
                        Password = parsedContent.password,
                        site_status = parsedContent.site_status,
                        IsTestUser = true
                    };

                    await _context.MLUsers.AddAsync(newTestUser);
                    await _context.SaveChangesAsync();

                    return new MLUserDTO()
                    {
                        MLid = parsedContent.id,
                        Nickname = parsedContent.nickname,
                        password = parsedContent.password,
                        MLIdSite = mlsite.MLId,
                    };
                }
                else
                {
                    throw new Exception($"error adding test user, response status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
