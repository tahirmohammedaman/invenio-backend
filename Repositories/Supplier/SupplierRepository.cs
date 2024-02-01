using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Supplier;

public class SupplierRepository: RepositoryBase<Models.Supplier>, ISupplierRepository
{
    public SupplierRepository(InvenioContext context) : base(context)
    {
    }
    
    public IEnumerable<Models.Supplier> GetAllSuppliers() =>
        FindAll()
            .OrderBy(supplier => supplier.Name)
            .ToList();
    
    public Models.Supplier? GetSupplierById(Guid id) =>
        FindByCondition(supplier => supplier.SupplierId.Equals(id))
            .FirstOrDefault();
    
    public void CreateSupplier(Models.Supplier supplier) => Create(supplier);
    
    public void UpdateSupplier(Models.Supplier supplier) => Context.Entry(supplier).State = EntityState.Modified;
    
    public void DeleteSupplier(Models.Supplier supplier) => Delete(supplier);
}