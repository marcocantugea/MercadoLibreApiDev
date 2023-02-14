using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MLApiContractors;

namespace MercadoLibreApiDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLCatalogController : ControllerBase
    {
        private ISiteService _siteService;
        private IServiceScopeFactory _serviceScopeFactory;

        public MLCatalogController(ISiteService siteService, IServiceScopeFactory serviceScopeFactory)
        {
            _siteService = siteService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPost("sites/load")]
        public async Task<IActionResult> LoadSitesToDb()
        {
            try
            {
                _ =Task.Run(async () => {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var siteService = scope.ServiceProvider.GetRequiredService<ISiteService>();
                        await siteService.LoadSitesToDb();

                    }
                });

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
