using invenio.Data;
using invenio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace invenio.Repositories.SaleOrder;

public class SaleOrderRepository : RepositoryBase<Models.SaleOrder>, ISaleOrderRepository
{
    public SaleOrderRepository(InvenioContext context) : base(context) {}

    public IEnumerable<Models.SaleOrder> GetAllSaleOrders() =>
        FindAll()
            .OrderBy(x => x.OrderPrice)
            .ToList();
    
    public Models.SaleOrder? GetSaleOrderById(Guid id) =>
        FindByCondition(saleOrder => saleOrder.Id.Equals(id))
            .FirstOrDefault();
    
    public void CreateSaleOrder(Models.SaleOrder saleOrder) => Create(saleOrder);
    
    public void UpdateSaleOrder(Models.SaleOrder saleOrder) => Context.Entry(saleOrder).State = EntityState.Modified;
    
    public void DeleteSaleOrder(Models.SaleOrder saleOrder) => Delete(saleOrder);
}
