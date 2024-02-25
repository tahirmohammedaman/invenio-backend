using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.SupplyOrder;

public class SupplyOrderRepository : RepositoryBase<Models.SupplyOrder>, ISupplyOrderRepository
{
    public SupplyOrderRepository(InvenioContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Models.SupplyOrder>> GetAllSupplyOrders() =>
        await FindAll()
            .Include(supplyOrder => supplyOrder.Warehouse)
            .Include(supplyOrder => supplyOrder.Supply)
            .Include(supplyOrder => supplyOrder.Supply.Product)
            .Include(supplyOrder => supplyOrder.Supply.Supplier)
            .OrderByDescending(supplyOrder => supplyOrder.OrderDate)
            .ToListAsync();
    
    public async Task<Models.SupplyOrder?> GetSupplyOrderById(Guid id) =>
        await FindByCondition(supplyOrder => supplyOrder.SupplyOrderId.Equals(id))
            .Include(supplyOrder => supplyOrder.Warehouse)
            .Include(supplyOrder => supplyOrder.Supply)
            .Include(supplyOrder => supplyOrder.Supply.Product)
            .Include(supplyOrder => supplyOrder.Supply.Supplier)
            .FirstOrDefaultAsync();
    
    public void CreateSupplyOrder(Models.SupplyOrder supplyOrder) => Create(supplyOrder);
    
    public void UpdateSupplyOrder(Models.SupplyOrder supplyOrder) => Context.Entry(supplyOrder).State = EntityState.Modified;
    
    public void DeleteSupplyOrder(Models.SupplyOrder supplyOrder) => Delete(supplyOrder);
}