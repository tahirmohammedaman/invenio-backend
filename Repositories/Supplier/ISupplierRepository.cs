namespace invenio.Repositories.Supplier;

public interface ISupplierRepository
{
    IEnumerable<Models.Supplier> GetAllSuppliers();
    Models.Supplier? GetSupplierById(Guid id);
    void CreateSupplier(Models.Supplier supplier);
    void UpdateSupplier(Models.Supplier supplier);
    void DeleteSupplier(Models.Supplier supplier);
}