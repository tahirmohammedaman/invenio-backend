using AutoMapper;
using invenio.Models.Dtos.Category;
using invenio.Repositories;
using invenio.Services;
using invenio.Services.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace invenio.Controllers;

[ApiController]
[Authorize]
[Route("/api/categories")]
public class CategoryController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public CategoryController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        try
        {
            var categories = await _repository.Category.GetAllCategories();
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
    [EnableQuery]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(Guid id)
    {
        try
        {
            var category = await _repository.Category.GetCategoryById(id);

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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryDto>> CreateCategory([FromForm] CreateCategoryDto categoryDto)
    {
        try
        {
            var category = _mapper.Map<Models.Category>(categoryDto);
            if (categoryDto.Image is not null)
                category.ImagePath = FileService.UploadFile(categoryDto.Image);
            
            _repository.Category.CreateCategory(category);
            await _repository.SaveAsync();
            
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        try
        {
            var category = await _repository.Category.GetCategoryById(id);
            if (category is null)
                return NotFound();
            
            if (category.ImagePath is not null)
                FileService.DeleteFile(category.ImagePath);
            
            _repository.Category.DeleteCategory(category);
            await _repository.SaveAsync();
            
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateCategory(Guid id, [FromForm] UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var category = await _repository.Category.GetCategoryById(id);
            if (category is null)
                return NotFound();
            
            _mapper.Map(updateCategoryDto, category);
            if (updateCategoryDto.Image is not null)
            {
                if (category.ImagePath is not null)
                    FileService.DeleteFile(category.ImagePath);
                category.ImagePath = FileService.UploadFile(updateCategoryDto.Image);
            }
            
            _repository.Category.UpdateCategory(category);
            await _repository.SaveAsync(); 
            
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    
}