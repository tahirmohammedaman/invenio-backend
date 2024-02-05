using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace invenio.Repositories.User;

public interface IUserRepository
{
    void CreateUser(Models.User user);
    Models.User? GetByEmail(string email);
}