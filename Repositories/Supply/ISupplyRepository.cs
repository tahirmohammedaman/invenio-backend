namespace invenio.Repositories.Supply;

public interface ISupplyRepository : IRepositoryBase<Models.Supply>
{
    Task<IEnumerable<Models.Supply>> GetAllSupplies();
    Task<Models.Supply?> GetSupplyById(Guid id);
    void CreateSupply(Models.Supply supply);
    void UpdateSupply(Models.Supply supply);
    void DeleteSupply(Models.Supply supply);
}