using Microsoft.AspNetCore.Mvc;
using MLApi.DTO;
using MLApiContractors;
using MLApiServices;

namespace MercadoLibreApiDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GlobalConfigController : Controller
    {
        private IGlobalConfigurationServices _service;

        public GlobalConfigController(IGlobalConfigurationServices service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> AddClientAndSecretConfiguration([FromBody] GlobalConfigurationDTO configurations)
        {
            try
            {
                await _service.SaveClientAndSecretConfigurations(configurations.ClientID, configurations.ClientSecret);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> SaveTCode([FromQuery] string code)
        {
            try
            {
                await _service.SaveCode(code);
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("authorize")]
        public async Task<ActionResult<string>> GetAuthLink()
        {
            try
            {
                var link= await _service.GetAuthLink("https://localhost:7192/GlobalConfig");
                return Ok(link);
            }
            catch (Exception ex)
            {

                return StatusCode(500,ex.Message);
            }
        }
    }
}
