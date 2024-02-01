using invenio.Repositories.Category;
using invenio.Repositories.Product;
using invenio.Repositories.Supplier;

namespace invenio.Repositories;

public interface IRepositoryWrapper
{
    IProductRepository Product { get; }
    
    ICategoryRepository Category { get; }
    
    ISupplierRepository Supplier { get; }
    
    void Save();
}