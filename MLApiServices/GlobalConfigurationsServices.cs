using DataEF.Context;
using DataEF.Models.Global;
using Microsoft.AspNetCore.Mvc;
using MLApi.DTO;
using MLApiConnector.Contractors;
using MLApiConnector.SerializedResponses;
using MLApiContractors;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLApiServices
{
    public class GlobalConfigurationsServices : IGlobalConfigurationServices
    {
        private MLApiDbContext _context;
        private IAuthResource _authResource;

        public GlobalConfigurationsServices(MLApiDbContext context,IAuthResource authResource)
        {
            _context = context;
            _authResource = authResource;
        }

        public async Task<string> GetAuthLink(string redirectUrl)
        {
            try
            {
                var clientid=_context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_ID").Select(gc => gc.Value).FirstOrDefault();

                if (string.IsNullOrEmpty(clientid)) throw new Exception("client id not found");

                return $"https://auth.mercadolibre.com.mx/authorization?response_type=code&client_id={clientid}&redirect_uri={redirectUrl}";
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task SaveClientAndSecretConfigurations(string clientId, string clientSecret)
        {
            try
            {
                if (string.IsNullOrEmpty(clientId)) throw new Exception("client id is empty");
                if (string.IsNullOrEmpty(clientSecret)) throw new Exception("client secret is empty");

                var clientIdRecord = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_ID").FirstOrDefault();
                var clientSecretRecord = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_SECRET").FirstOrDefault();
                
                if (clientIdRecord == null) throw new Exception("global variable not found");
                if (clientSecretRecord == null) throw new Exception("global variable not found");

                clientIdRecord.Value = clientId;
                clientIdRecord.updated = DateTime.Now;
                clientSecretRecord.Value= clientSecret;
                clientSecretRecord.updated = DateTime.Now;

                _context.GlobalConfigurations.Update(clientIdRecord);
                _context.GlobalConfigurations.Update(clientSecretRecord);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveCode(string tcode)
        {
            try
            {
                if (String.IsNullOrEmpty(tcode)) throw new Exception("invalid code");

                var mlcode = _context.GlobalConfigurations.Where(gc => gc.Name == "ML_CODE").FirstOrDefault();

                if (mlcode == null) throw new Exception("global variable not found");

                mlcode.Value = tcode;
                mlcode.updated = DateTime.Now; 
                _context.GlobalConfigurations.Update(mlcode);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task SaveTokenInfo(string accessToken,int expireIn, int userId, string RefreshToken)
        {
            try
            {
                if (String.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(RefreshToken)) throw new Exception("access token or refresh token is empty");
                if (expireIn <= 0 || userId <= 0) throw new Exception("expire time or user id is invalid");

                var dateExpire= DateTime.Now.AddSeconds(expireIn);

                var itemsConfig= _context.GlobalConfigurations.Where(gc=> gc.Name.Contains("TOKEN")).ToList();
                if (itemsConfig.Count <= 0) throw new Exception("no global configurations found");

                var accessTokenDb = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN").FirstOrDefault();
                accessTokenDb.Value = accessToken;
                accessTokenDb.updated = DateTime.Now;
                _context.GlobalConfigurations.Update(accessTokenDb);

                var accessTokenExpireDb = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE").FirstOrDefault();
                accessTokenExpireDb.Value = expireIn.ToString();
                accessTokenExpireDb.updated = DateTime.Now;
                _context.GlobalConfigurations.Update(accessTokenDb);

                var userIdDb = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_USERID").FirstOrDefault();
                userIdDb.Value = userId.ToString();
                userIdDb.updated = DateTime.Now;
                _context.GlobalConfigurations.Update(userIdDb);

                var refreshTokenDb = itemsConfig.Where(i => i.Name == "REFRESH_TOKEN").FirstOrDefault();
                refreshTokenDb.Value = RefreshToken;
                refreshTokenDb.updated = DateTime.Now;
                _context.GlobalConfigurations.Update(refreshTokenDb);

                var dateExpireDb = itemsConfig.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").FirstOrDefault();
                dateExpireDb.Value = dateExpire.ToString("yyyy-MM-dd HH:mm:ss");
                dateExpireDb.updated = DateTime.Now;
                _context.GlobalConfigurations.Update(dateExpireDb);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TokenInfoDTO> GetAuthToken()
        {
            //obtenemos el token de la base de datos
            // si esta vacio solicitamos el token al recurso
            // si no esta vacio revisamos que aun este vigente, el caso 
            // de que no este vigente, solicitamos una actualizacion
            // guardmos el token acutalizado y devolvemos el token con la informacion

            var token= _context.GlobalConfigurations.Where(gc=>gc.Name== "ACCESS_TOKEN").Select(global=> global.Value).FirstOrDefault();
            var expireDateToken = _context.GlobalConfigurations.Where(gc => gc.Name == "ACCESS_TOKEN_EXPIRE_DATE").Select(global => global.Value).FirstOrDefault();
            if (String.IsNullOrEmpty(token))
            {
                //obtenemos el token del recurso
                var clientid = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_ID").Select(global => global.Value).FirstOrDefault();
                var clientSecret = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_SECRET").Select(global => global.Value).FirstOrDefault();
                var code = _context.GlobalConfigurations.Where(gc => gc.Name == "ML_CODE").Select(global => global.Value).FirstOrDefault();

                try
                {
                    var response = _authResource.GetToken(clientid, clientSecret, code, "https://localhost:7192/GlobalConfig");
                    if (response.IsSuccessStatusCode)
                    {
                        var tokenInfo = JsonSerializer.Deserialize<TokenResponse>(response.Content);
                        await SaveTokenInfo(tokenInfo.access_token, tokenInfo.expires_in, tokenInfo.user_id, tokenInfo.refresh_token);
                        var expireDate = _context.GlobalConfigurations.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").Select(i => i.Value).FirstOrDefault();
                        return new TokenInfoDTO()
                        {
                            tokenAccess = tokenInfo.access_token,
                            expireIn = expireDate
                        };
                    }
                    else
                    {
                        throw new Exception($"error getting token status response {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }

            if(expireDateToken == null) return await RefreshToken();

            if (DateTime.TryParse(expireDateToken, out DateTime result)==false) return await RefreshToken();
            
            if (DateTime.Parse(expireDateToken)<DateTime.Now) return await RefreshToken();

            return new TokenInfoDTO()
            {
                tokenAccess= token,
                expireIn = expireDateToken,
            };
        }

        public async Task<TokenInfoDTO> RefreshToken()
        {
            var refrestoken = _context.GlobalConfigurations.Where(gc => gc.Name == "REFRESH_TOKEN").Select(global => global.Value).FirstOrDefault();
            if (String.IsNullOrEmpty(refrestoken)) throw new Exception("invalid refresh token");

            try
            {
                var clientid = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_ID").Select(global => global.Value).FirstOrDefault();
                var clientSecret = _context.GlobalConfigurations.Where(gc => gc.Name == "CLIENT_SECRET").Select(global => global.Value).FirstOrDefault();
                var refreshToken = _context.GlobalConfigurations.Where(gc => gc.Name == "REFRESH_TOKEN").Select(global => global.Value).FirstOrDefault();
                var response = _authResource.GetRefreshToken(clientid, clientSecret, refreshToken);
                if(response.IsSuccessStatusCode)
                {
                    var tokenInfo = JsonSerializer.Deserialize<TokenResponse>(response.Content);
                    await SaveTokenInfo(tokenInfo.access_token, tokenInfo.expires_in, tokenInfo.user_id, tokenInfo.refresh_token);
                    var expireDate = _context.GlobalConfigurations.Where(i => i.Name == "ACCESS_TOKEN_EXPIRE_DATE").Select(i => i.Value).FirstOrDefault();
                    return new TokenInfoDTO()
                    {
                        tokenAccess = tokenInfo.access_token,
                        expireIn = expireDate
                    };
                }
                else
                {
                    throw new Exception($"error getting token status response {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
