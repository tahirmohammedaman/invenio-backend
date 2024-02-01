namespace invenio.Repositories.SupplyOrder;

public interface ISupplyOrderRepository : IRepositoryBase<Models.SupplyOrder>
{
    IEnumerable<Models.SupplyOrder> GetAllSupplyOrders();
    Models.SupplyOrder? GetSupplyOrderById(Guid id);
    void CreateSupplyOrder(Models.SupplyOrder supplyOrder);
    void UpdateSupplyOrder(Models.SupplyOrder supplyOrder);
    void DeleteSupplyOrder(Models.SupplyOrder supplyOrder);
}