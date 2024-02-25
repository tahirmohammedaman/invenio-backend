namespace invenio.Repositories.Warehouse;

public interface IWarehouseRepository : IRepositoryBase<Models.Warehouse>
{
    Task<IEnumerable<Models.Warehouse>> GetAllWarehouses();
    Task<Models.Warehouse?> GetWarehouseById(Guid id);
    void CreateWarehouse(Models.Warehouse warehouse);
    void UpdateWarehouse(Models.Warehouse warehouse);
    void DeleteWarehouse(Models.Warehouse warehouse);
}
