using invenio.Data;
using invenio.Repositories.Category;
using invenio.Repositories.Product;
using invenio.Repositories.Supplier;

namespace invenio.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly InvenioContext _context;
    
    private IProductRepository _product;
    private ICategoryRepository _category;
    private ISupplierRepository _supplier;
    
    public RepositoryWrapper(InvenioContext context)
    {
        _context = context;
    }

    public IProductRepository Product
    {
        get
        {
            if (_product is null)
                _product = new ProductRepository(_context);
            
            return _product;
        }
    }

    public ICategoryRepository Category
    {
        get
        {
            if (_category is null)
                _category = new CategoryRepository(_context);
            
            return _category;
        }
    }

    public ISupplierRepository Supplier
    {
        get
        {
            if (_supplier is null)
                _supplier = new SupplierRepository(_context);
            
            return _supplier;
        }
    }
    
    public void Save()
    {
        _context.SaveChanges();
    }
}