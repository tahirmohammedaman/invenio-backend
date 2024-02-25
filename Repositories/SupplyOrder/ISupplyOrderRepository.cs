namespace invenio.Repositories.SupplyOrder;

public interface ISupplyOrderRepository : IRepositoryBase<Models.SupplyOrder>
{
    Task<IEnumerable<Models.SupplyOrder>> GetAllSupplyOrders();
    Task<Models.SupplyOrder?> GetSupplyOrderById(Guid id);
    void CreateSupplyOrder(Models.SupplyOrder supplyOrder);
    void UpdateSupplyOrder(Models.SupplyOrder supplyOrder);
    void DeleteSupplyOrder(Models.SupplyOrder supplyOrder);
}