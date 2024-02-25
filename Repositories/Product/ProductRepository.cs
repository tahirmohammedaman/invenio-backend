using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Product;

public class ProductRepository: RepositoryBase<Models.Product>, IProductRepository
{
    public ProductRepository(InvenioContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Models.Product>> GetAllProducts() =>
        await FindAll()
            .OrderBy(product => product.Name)
            .Include(product => product.Category)
            .Include(product => product.SaleOrders) // Include SaleOrders relation
            .ThenInclude(saleOrder => saleOrder.Customer) // Include Customer relation within SaleOrders
            .Include(product => product.Supplies) // Include Supplies relation
            .ThenInclude(supply => supply.Supplier) // Include Supplier relation within Supplies
            .ToListAsync();
    
    public async Task<Models.Product?> GetProductById(Guid id) =>
        await FindByCondition(product => product.ProductId.Equals(id))
            .Include(product => product.Category)
            .Include(product => product.Supplies)
            .Include(product => product.Stocks)
            .FirstOrDefaultAsync();
    
    public void CreateProduct(Models.Product product) => Create(product);
    
    public void UpdateProduct(Models.Product product) => Context.Entry(product).State = EntityState.Modified;
    
    public void DeleteProduct(Models.Product product) => Delete(product);
    
}