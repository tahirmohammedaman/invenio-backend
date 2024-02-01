using invenio.Models;
using Microsoft.EntityFrameworkCore;

namespace invenio.Data;

public class InvenioContext : DbContext
{
    public InvenioContext(DbContextOptions<InvenioContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<Category> Categories => Set<Category>();
    
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    
    public DbSet<Supply> Supplies => Set<Supply>();
    
    public DbSet<Stock> Stocks => Set<Stock>();
    
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
}