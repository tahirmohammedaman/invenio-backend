namespace invenio.Repositories.Stock;

public interface IStockRepository : IRepositoryBase<Models.Stock>
{
    Task<IEnumerable<Models.Stock>> GetAllStocks();
    Task<Models.Stock?> GetStockById(Guid id);
    void CreateStock(Models.Stock stock);
    void UpdateStock(Models.Stock stock);
    void DeleteStock(Models.Stock stock);
    
    Task<Models.Stock?> GetStockByProductIdAndWarehouseId(Guid productId, Guid warehouseId);
}