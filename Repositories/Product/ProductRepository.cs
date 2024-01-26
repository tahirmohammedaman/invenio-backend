using invenio.Data;

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
    
    public Models.Product GetProductById(Guid id) =>
        FindByCondition(product => product.ProductId.Equals(id))
            .FirstOrDefault();
    
    public void CreateProduct(Models.Product product) => Create(product);
    
    public void UpdateProduct(Models.Product product) => Update(product);
    
    public void DeleteProduct(Models.Product product) => Delete(product);
    
}