using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.SaleOrder;

public class SaleOrderRepository : RepositoryBase<Models.SaleOrder>, ISaleOrderRepository
{
    public SaleOrderRepository(InvenioContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Models.SaleOrder>> GetAllSaleOrders() =>
        await FindAll()
            .Include(saleOrder => saleOrder.Customer)
            .Include(saleOrder => saleOrder.Warehouse)
            .Include(saleOrder => saleOrder.Product)
            .ThenInclude(product => product.Category)
            .OrderByDescending(saleOrder => saleOrder.OrderDate)
            .ToListAsync();
    
    public async Task<Models.SaleOrder?> GetSaleOrderById(Guid id) =>
        await FindByCondition(saleOrder => saleOrder.SaleOrderId.Equals(id))
            .Include(saleOrder => saleOrder.Customer)
            .Include(saleOrder => saleOrder.Product)
            .Include(saleOrder => saleOrder.Warehouse)
            .FirstOrDefaultAsync();
    
    public void CreateSaleOrder(Models.SaleOrder saleOrder) => Create(saleOrder);
    
    public void UpdateSaleOrder(Models.SaleOrder saleOrder) => Context.Entry(saleOrder).State = EntityState.Modified;
    
    public void DeleteSaleOrder(Models.SaleOrder saleOrder) => Delete(saleOrder);
}