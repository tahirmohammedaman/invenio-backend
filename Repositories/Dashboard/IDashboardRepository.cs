using invenio.Models.Dtos;

namespace invenio.Repositories.Dashboard;

public interface IDashboardRepository
{
    DashboardDto GetDashboardData();
}