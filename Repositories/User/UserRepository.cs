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
    
    public async Task CreateUser(Models.User user)
    {
        await Context.Set<Models.User>().AddAsync(user);
    }

    public async Task<Models.User?> GetByEmail(string email) =>
        await Context.Set<Models.User>().AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IEnumerable<Models.User>> GetAllUsers() =>
        await Context.Set<Models.User>().AsNoTracking().ToListAsync();
}