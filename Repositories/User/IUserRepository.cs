
namespace invenio.Repositories.User;

public interface IUserRepository
{
    Task CreateUser(Models.User user);
    Task<Models.User?> GetByEmail(string email);
    Task<IEnumerable<Models.User>> GetAllUsers();
}