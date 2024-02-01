namespace invenio.Repositories.Stock;

public interface IStockRepository : IRepositoryBase<Models.Stock>
{
    IEnumerable<Models.Stock> GetAllStocks();
    Models.Stock? GetStockById(Guid id);
    void CreateStock(Models.Stock stock);
    void UpdateStock(Models.Stock stock);
    void DeleteStock(Models.Stock stock);
}