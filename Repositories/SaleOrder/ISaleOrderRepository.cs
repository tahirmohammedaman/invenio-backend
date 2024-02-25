namespace invenio.Repositories.SaleOrder;

public interface ISaleOrderRepository : IRepositoryBase<Models.SaleOrder>
{
    Task<IEnumerable<Models.SaleOrder>> GetAllSaleOrders();
    Task<Models.SaleOrder?> GetSaleOrderById(Guid id);
    void CreateSaleOrder(Models.SaleOrder saleOrder);
    void UpdateSaleOrder(Models.SaleOrder saleOrder);
    void DeleteSaleOrder(Models.SaleOrder saleOrder);
}