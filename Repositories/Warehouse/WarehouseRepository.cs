using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Warehouse;

public class WarehouseRepository : RepositoryBase<Models.Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(InvenioContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Models.Warehouse>> GetAllWarehouses() =>
        await FindAll()
            .OrderBy(warehouse => warehouse.Name)
            .ToListAsync();
    
    public async Task<Models.Warehouse?> GetWarehouseById(Guid id) =>
        await FindByCondition(warehouse => warehouse.WarehouseId.Equals(id))
            .FirstOrDefaultAsync();
    
    public void CreateWarehouse(Models.Warehouse warehouse) => Create(warehouse);
    
    public void UpdateWarehouse(Models.Warehouse warehouse) => Context.Entry(warehouse).State = EntityState.Modified;
    
    public void DeleteWarehouse(Models.Warehouse warehouse) => Delete(warehouse);
    
}