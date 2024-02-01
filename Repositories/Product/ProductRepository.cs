using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Product;

public class ProductRepository: RepositoryBase<Models.Product>, IProductRepository
{
    public ProductRepository(InvenioContext context) : base(context)
    {
    }
    
    public IEnumerable<Models.Product> GetAllProducts() =>
        FindAll()
            .OrderBy(product => product.Name)
            .ToList();
    
    public Models.Product? GetProductById(Guid id) =>
        FindByCondition(product => product.ProductId.Equals(id))
            .Include(product => product.Category)
            .Include(product => product.Supplies)
            .FirstOrDefault();
    
    public void CreateProduct(Models.Product product) => Create(product);
    
    public void UpdateProduct(Models.Product product) => Context.Entry(product).State = EntityState.Modified;
    
    public void DeleteProduct(Models.Product product) => Delete(product);
    
}