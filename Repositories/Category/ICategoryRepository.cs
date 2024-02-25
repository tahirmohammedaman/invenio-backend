namespace invenio.Repositories.Category;

public interface ICategoryRepository : IRepositoryBase<Models.Category>
{
    Task<IEnumerable<Models.Category>> GetAllCategories();
    Task<Models.Category?> GetCategoryById(Guid id);
    void CreateCategory(Models.Category category);
    void UpdateCategory(Models.Category category);
    void DeleteCategory(Models.Category category);
}