using invenio.Data;
using invenio.Repositories.Category;
using invenio.Repositories.Product;

namespace invenio.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly InvenioContext _context;
    
    private IProductRepository _product;
    private ICategoryRepository _category;
    
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
    
    public void Save()
    {
        _context.SaveChanges();
    }
}