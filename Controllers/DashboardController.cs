using invenio.Models.Dtos;
using invenio.Repositories.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace invenio.Controllers;

[ApiController]
[Authorize]
[Route("/api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardRepository _repository;
    
    public DashboardController(IDashboardRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboardData()
    {
        try
        {
            var dashboardData = await _repository.GetDashboardData();
            return Ok(dashboardData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
}