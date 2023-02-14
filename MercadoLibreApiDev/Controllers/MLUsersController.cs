using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MLApiContractors;

namespace MercadoLibreApiDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLUsersController : ControllerBase
    {
        private IUserService _userService;

        public MLUsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add/testuser")]
        public async Task<IActionResult> AddTestUser([FromQuery] string mlsite)
        {
            try
            {
                var response= await _userService.AddTestUser(mlsite);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
