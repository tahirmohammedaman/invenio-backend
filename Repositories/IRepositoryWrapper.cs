using invenio.Repositories.Category;
using invenio.Repositories.Product;
using invenio.Repositories.Stock;
using invenio.Repositories.Supplier;
using invenio.Repositories.Supply;

namespace invenio.Repositories;

public interface IRepositoryWrapper
{
    IProductRepository Product { get; }
    
    ICategoryRepository Category { get; }
    
    ISupplierRepository Supplier { get; }
    
    ISupplyRepository Supply { get; }
    
    IStockRepository Stock { get; }
    
    void Save();
}