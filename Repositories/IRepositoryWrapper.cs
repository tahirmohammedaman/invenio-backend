using invenio.Repositories.Category;
using invenio.Repositories.Product;

namespace invenio.Repositories;

public interface IRepositoryWrapper
{
    IProductRepository Product { get; }
    
    ICategoryRepository Category { get; }
    
    void Save();
}