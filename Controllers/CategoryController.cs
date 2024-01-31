using AutoMapper;
using invenio.Models.Dtos.Category;
using invenio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace invenio.Controllers;

[ApiController]
[Route("/api/categories")]
public class CategoryController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public CategoryController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<CategoryDto>> GetAll()
    {
        try
        {
            var categories = _repository.Category.GetAllCategories();
            var categoriesResponse = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            
            return Ok(categoriesResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    public ActionResult<CategoryDto> GetCategoryById(Guid id)
    {
        try
        {
            var category = _repository.Category.GetCategoryById(id);

            if (category is null)
                return NotFound();

            var categoryResponse = _mapper.Map<CategoryDto>(category);
            return Ok(categoryResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    public ActionResult<CategoryDto> CreateCategory(CreateCategoryDto categoryDto)
    {
        try
        {
            var category = _mapper.Map<Models.Category>(categoryDto);
            _repository.Category.CreateCategory(category);
            _repository.Save();
            
            var categoryResponse = _mapper.Map<CategoryDto>(category);
            return CreatedAtAction(nameof(GetCategoryById), new {id = categoryResponse.CategoryId}, categoryResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(Guid id)
    {
        try
        {
            var category = _repository.Category.GetCategoryById(id);
            if (category is null)
                return NotFound();
            
            _repository.Category.DeleteCategory(category);
            _repository.Save();
            
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPut("{id}")]
    public ActionResult UpdateCategory(Guid id, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var category = _repository.Category.GetCategoryById(id);
            if (category is null)
                return NotFound();
            
            _mapper.Map(updateCategoryDto, category);
            _repository.Category.UpdateCategory(category);
            _repository.Save(); 
            
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    
}