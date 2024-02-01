namespace invenio.Repositories.Warehouse;

public interface IWarehouseRepository : IRepositoryBase<Models.Warehouse>
{
    IEnumerable<Models.Warehouse> GetAllWarehouses();
    Models.Warehouse? GetWarehouseById(Guid id);
    void CreateWarehouse(Models.Warehouse warehouse);
    void UpdateWarehouse(Models.Warehouse warehouse);
    void DeleteWarehouse(Models.Warehouse warehouse);
}
