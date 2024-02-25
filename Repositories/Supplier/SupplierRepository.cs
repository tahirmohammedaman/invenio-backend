using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Supplier;

public class SupplierRepository: RepositoryBase<Models.Supplier>, ISupplierRepository
{
    public SupplierRepository(InvenioContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Models.Supplier>> GetAllSuppliers() =>
        await FindAll()
            .OrderBy(supplier => supplier.Name)
            .Include(supplier => supplier.Supplies)
            .ThenInclude(supply => supply.Product)
            .Include(supplier => supplier.Supplies)
            .ThenInclude(supply => supply.SupplyOrders) // Include SupplyOrders relation within Supplies
            .ToListAsync();

    
    public async Task<Models.Supplier?> GetSupplierById(Guid id) =>
        await FindByCondition(supplier => supplier.SupplierId.Equals(id))
            .FirstOrDefaultAsync();
    
    public void CreateSupplier(Models.Supplier supplier) => Create(supplier);
    
    public void UpdateSupplier(Models.Supplier supplier) => Context.Entry(supplier).State = EntityState.Modified;
    
    public void DeleteSupplier(Models.Supplier supplier) => Delete(supplier);
}