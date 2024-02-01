using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.SupplyOrder;

public class SupplyOrderRepository : RepositoryBase<Models.SupplyOrder>, ISupplyOrderRepository
{
    public SupplyOrderRepository(InvenioContext context) : base(context)
    {
    }
    
    public IEnumerable<Models.SupplyOrder> GetAllSupplyOrders() =>
        FindAll()
            .Include(supplyOrder => supplyOrder.Warehouse)
            .Include(supplyOrder => supplyOrder.Supply)
            .Include(supplyOrder => supplyOrder.Supply.Product)
            .Include(supplyOrder => supplyOrder.Supply.Supplier)
            .OrderByDescending(supplyOrder => supplyOrder.OrderDate)
            .ToList();
    
    public Models.SupplyOrder? GetSupplyOrderById(Guid id) =>
        FindByCondition(supplyOrder => supplyOrder.SupplyOrderId.Equals(id))
            .Include(supplyOrder => supplyOrder.Warehouse)
            .Include(supplyOrder => supplyOrder.Supply)
            .Include(supplyOrder => supplyOrder.Supply.Product)
            .Include(supplyOrder => supplyOrder.Supply.Supplier)
            .FirstOrDefault();
    
    public void CreateSupplyOrder(Models.SupplyOrder supplyOrder) => Create(supplyOrder);
    
    public void UpdateSupplyOrder(Models.SupplyOrder supplyOrder) => Context.Entry(supplyOrder).State = EntityState.Modified;
    
    public void DeleteSupplyOrder(Models.SupplyOrder supplyOrder) => Delete(supplyOrder);
}