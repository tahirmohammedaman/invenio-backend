using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Supply;

public class SupplyRepository : RepositoryBase<Models.Supply>, ISupplyRepository
{
    public SupplyRepository(InvenioContext context) : base(context)
    {
    }
    
    public IEnumerable<Models.Supply> GetAllSupplies() =>
        FindAll()
            .OrderBy(supply => supply.Supplier.Name)
            .Include(supply => supply.Supplier)
            .ToList();
    
    public Models.Supply? GetSupplyById(Guid id) =>
        FindByCondition(supply => supply.SupplyId.Equals(id))
            .Include(supply => supply.Supplier)
            .Include(supply => supply.Product)
            .FirstOrDefault();
    
    public void CreateSupply(Models.Supply supply) => Create(supply);
    
    public void UpdateSupply(Models.Supply supply) => Context.Entry(supply).State = EntityState.Modified;
    
    public void DeleteSupply(Models.Supply supply) => Delete(supply);
}