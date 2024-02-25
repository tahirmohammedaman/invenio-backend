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

public interface IRepositoryWrapper
{
    IProductRepository Product { get; }
    
    ICategoryRepository Category { get; }
    
    ISupplierRepository Supplier { get; }
    
    ISupplyRepository Supply { get; }
    
    IStockRepository Stock { get; }
    
    IWarehouseRepository Warehouse { get; }
    
    ISupplyOrderRepository SupplyOrder { get; }
    
    IUserRepository User { get; }
    
    ICustomerRepository Customer { get; }
    
    ISaleOrderRepository SaleOrder { get; }
    
    Task SaveAsync();
}