using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Stock;

public class StockRepository : RepositoryBase<Models.Stock>, IStockRepository
{
    public StockRepository(InvenioContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Models.Stock>> GetAllStocks() =>
        await FindAll()
            .OrderBy(stock => stock.Product.Name)
            .Include(stock => stock.Warehouse)
            .Include(stock => stock.Product)
            .ThenInclude(product => product.Category)
            .ToListAsync();
    
    public async Task<Models.Stock?> GetStockById(Guid id) =>
        await FindByCondition(stock => stock.StockId.Equals(id))
            .Include(stock => stock.Product)
            .ThenInclude(product => product.Category)
            .FirstOrDefaultAsync();
    
    public void CreateStock(Models.Stock stock) => Create(stock);
    
    public void UpdateStock(Models.Stock stock) => Context.Entry(stock).State = EntityState.Modified;
    
    public void DeleteStock(Models.Stock stock) => Delete(stock);
    
    public async Task<Models.Stock?> GetStockByProductIdAndWarehouseId(Guid productId, Guid warehouseId) =>
        await FindByCondition(stock => stock.ProductId.Equals(productId) && stock.WarehouseId.Equals(warehouseId))
            .Include(stock => stock.Product)
            .ThenInclude(product => product.Category)
            .FirstOrDefaultAsync();
}