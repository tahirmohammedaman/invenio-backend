using invenio.Models;
using Microsoft.EntityFrameworkCore;

namespace invenio.Data;

public class InvenioContext : DbContext
{
    public InvenioContext(DbContextOptions<InvenioContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products => Set<Product>();
}