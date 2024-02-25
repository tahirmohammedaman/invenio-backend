namespace invenio.Repositories.Supplier;

public interface ISupplierRepository
{
    Task<IEnumerable<Models.Supplier>> GetAllSuppliers();
    Task<Models.Supplier?> GetSupplierById(Guid id);
    void CreateSupplier(Models.Supplier supplier);
    void UpdateSupplier(Models.Supplier supplier);
    void DeleteSupplier(Models.Supplier supplier);
}