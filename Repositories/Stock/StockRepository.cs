using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Stock;

public class StockRepository : RepositoryBase<Models.Stock>, IStockRepository
{
    public StockRepository(InvenioContext context) : base(context)
    {
    }
    
    public IEnumerable<Models.Stock> GetAllStocks() =>
        FindAll()
            .OrderBy(stock => stock.Product.Name)
            .Include(stock => stock.Warehouse)
            .Include(stock => stock.Product)
            .ThenInclude(product => product.Category)
            .ToList();
    
    public Models.Stock? GetStockById(Guid id) =>
        FindByCondition(stock => stock.StockId.Equals(id))
            .Include(stock => stock.Product)
            .ThenInclude(product => product.Category)
            .FirstOrDefault();
    
    public void CreateStock(Models.Stock stock) => Create(stock);
    
    public void UpdateStock(Models.Stock stock) => Context.Entry(stock).State = EntityState.Modified;
    
    public void DeleteStock(Models.Stock stock) => Delete(stock);
    
    public Models.Stock? GetStockByProductIdAndWarehouseId(Guid productId, Guid warehouseId) =>
        FindByCondition(stock => stock.ProductId.Equals(productId) && stock.WarehouseId.Equals(warehouseId))
            .Include(stock => stock.Product)
            .ThenInclude(product => product.Category)
            .FirstOrDefault();
}