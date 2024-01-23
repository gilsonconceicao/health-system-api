using Microsoft.AspNetCore.Mvc;

namespace HealthSystem.Web.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class ManagementHealth : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() 
        {
            return Ok();
        }
    }
}