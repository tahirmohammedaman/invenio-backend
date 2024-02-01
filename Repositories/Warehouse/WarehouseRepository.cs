using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Warehouse;

public class WarehouseRepository : RepositoryBase<Models.Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(InvenioContext context) : base(context)
    {
    }
    
    public IEnumerable<Models.Warehouse> GetAllWarehouses() =>
        FindAll()
            .OrderBy(warehouse => warehouse.Name)
            .ToList();
    
    public Models.Warehouse? GetWarehouseById(Guid id) =>
        FindByCondition(warehouse => warehouse.WarehouseId.Equals(id))
            .FirstOrDefault();
    
    public void CreateWarehouse(Models.Warehouse warehouse) => Create(warehouse);
    
    public void UpdateWarehouse(Models.Warehouse warehouse) => Context.Entry(warehouse).State = EntityState.Modified;
    
    public void DeleteWarehouse(Models.Warehouse warehouse) => Delete(warehouse);
    
}