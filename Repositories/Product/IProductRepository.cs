namespace invenio.Repositories.Product;

public interface IProductRepository : IRepositoryBase<Models.Product>
{
    IEnumerable<Models.Product> GetAllProducts();
    Models.Product? GetProductById(Guid id);
    void CreateProduct(Models.Product product);
    void UpdateProduct(Models.Product product);
    void DeleteProduct(Models.Product product);
}