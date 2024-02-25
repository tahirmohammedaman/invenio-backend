using invenio.Models.Dtos;

namespace invenio.Repositories.Dashboard;

public interface IDashboardRepository
{
    Task<DashboardDto> GetDashboardData();
}