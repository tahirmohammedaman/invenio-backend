namespace invenio.Repositories.Product;

public interface IProductRepository : IRepositoryBase<Models.Product>
{
    Task<IEnumerable<Models.Product>> GetAllProducts();
    Task<Models.Product?> GetProductById(Guid id);
    void CreateProduct(Models.Product product);
    void UpdateProduct(Models.Product product);
    void DeleteProduct(Models.Product product);
}