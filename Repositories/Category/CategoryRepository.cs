using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Category;

public class CategoryRepository : RepositoryBase<Models.Category>, ICategoryRepository
{
    public CategoryRepository(InvenioContext context) : base(context)
    {
    }
    
    public IEnumerable<Models.Category> GetAllCategories() =>
        FindAll()
            .OrderBy(category => category.Name)
            .Include(category => category.ParentCategory)
            .ToList();
    
    public Models.Category GetCategoryById(Guid id) =>
        FindByCondition(category => category.CategoryId.Equals(id))
            .Include(category => category.ParentCategory)
            .FirstOrDefault();
    
    public void CreateCategory(Models.Category category) => Create(category);
    
    public void UpdateCategory(Models.Category category) => Context.Entry(category).State = EntityState.Modified;
    
    public void DeleteCategory(Models.Category category) => Delete(category);
}