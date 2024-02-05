using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.User;

public class UserRepository : IUserRepository
{
    protected InvenioContext Context { get; }
    
    public UserRepository(InvenioContext context)
    {
        Context = context;
    }
    
    public void CreateUser(Models.User user)
    {
        Context.Set<Models.User>().Add(user);
    }

    public Models.User? GetByEmail(string email) =>
        Context.Set<Models.User>().AsNoTracking().First(u => u.Email == email);
}