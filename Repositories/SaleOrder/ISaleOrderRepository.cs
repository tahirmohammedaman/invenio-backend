namespace invenio.Repositories.SaleOrder;

public interface ISaleOrderRepository : IRepositoryBase<Models.SaleOrder>
{
    IEnumerable<Models.SaleOrder> GetAllSaleOrders();
    Models.SaleOrder? GetSaleOrderById(Guid id);
    void CreateSaleOrder(Models.SaleOrder saleOrder);
    void UpdateSaleOrder(Models.SaleOrder saleOrder);
    void DeleteSaleOrder(Models.SaleOrder saleOrder);
}