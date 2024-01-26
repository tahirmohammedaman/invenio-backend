using invenio.Data;
using invenio.Repositories.Product;

namespace invenio.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly InvenioContext _context;
    
    private IProductRepository _product;
    
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
    
    public void Save()
    {
        _context.SaveChanges();
    }
}