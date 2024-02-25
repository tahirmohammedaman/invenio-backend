using invenio.Data;
using invenio.Repositories.Category;
using invenio.Repositories.Customer;
using invenio.Repositories.Dashboard;
using invenio.Repositories.Product;
using invenio.Repositories.SaleOrder;
using invenio.Repositories.Stock;
using invenio.Repositories.Supplier;
using invenio.Repositories.Supply;
using invenio.Repositories.SupplyOrder;
using invenio.Repositories.User;
using invenio.Repositories.Warehouse;

namespace invenio.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly InvenioContext _context;
    
    private IProductRepository _product;
    private ICategoryRepository _category;
    private ISupplierRepository _supplier;
    private ISupplyRepository _supply;
    private IStockRepository _stock;
    private IWarehouseRepository _warehouse;
    private ISupplyOrderRepository _supplyOrder;
    private IUserRepository _user;
    private ICustomerRepository _customer;
    private ISaleOrderRepository _saleOrder;
    
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
    
    public IWarehouseRepository Warehouse
    {
        get
        {
            if (_warehouse is null)
                _warehouse = new WarehouseRepository(_context);
            
            return _warehouse;
        }
    }

    public ISupplyOrderRepository SupplyOrder
    {
        get
        {
            if (_supplyOrder is null)
                _supplyOrder = new SupplyOrderRepository(_context);

            return _supplyOrder;
        }
    }

    public IUserRepository User
    {
        get
        {
            if (_user is null)
                _user = new UserRepository(_context);
            
            return _user;
        }
    }
    
    public ICustomerRepository Customer
    {
        get
        {
            if (_customer is null)
                _customer = new CustomerRepository(_context);
            
            return _customer;
        }
    }
    
    public ISaleOrderRepository SaleOrder
    {
        get
        {
            if (_saleOrder is null)
                _saleOrder = new SaleOrderRepository(_context);
            
            return _saleOrder;
        }
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}