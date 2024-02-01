using invenio.Data;
using invenio.Repositories.Category;
using invenio.Repositories.Product;
using invenio.Repositories.Stock;
using invenio.Repositories.Supplier;
using invenio.Repositories.Supply;

namespace invenio.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly InvenioContext _context;
    
    private IProductRepository _product;
    private ICategoryRepository _category;
    private ISupplierRepository _supplier;
    private ISupplyRepository _supply;
    private IStockRepository _stock;
    
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
    
    public ISupplyRepository Supply
    {
        get
        {
            if (_supply is null)
                _supply = new SupplyRepository(_context);
            
            return _supply;
        }
    }
    
    public IStockRepository Stock
    {
        get
        {
            if (_stock is null)
                _stock = new StockRepository(_context);
            
            return _stock;
        }
    }
    
    public void Save()
    {
        _context.SaveChanges();
    }
}