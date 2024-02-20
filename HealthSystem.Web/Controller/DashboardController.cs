using HealthSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthSystem.Web.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardRepository _dashboardRepository;

        public DashboardController(DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }


        [HttpGet]
        public async Task<IActionResult> getDataDashboard()
        {
            try
            {
                return Ok(await _dashboardRepository.GetDashboardDataAppointment());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}