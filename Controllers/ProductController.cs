using AutoMapper;
using invenio.Models;
using invenio.Models.Dtos.Product;
using invenio.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace invenio.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public ProductController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<ProductDto>> GetAll()
    {
        try
        {
            var products = _repository.Product.GetAllProducts();
            var productsResponse = _mapper.Map<IEnumerable<ProductDto>>(products);
            
            return Ok(productsResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetProductById(Guid id)
    {
        try
        {
            var product = _repository.Product.GetProductById(id);

            if (product is null)
                return NotFound();

            var productResponse = _mapper.Map<ProductDto>(product);
            return Ok(productResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        try
        {
            if (createProductDto is null)
                return BadRequest("Product is null");

            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");

            var product = _mapper.Map<Product>(createProductDto);
            // product.ProductId= Guid.NewGuid();
            _repository.Product.CreateProduct(product);
            _repository.Save();

            var productDto = _mapper.Map<ProductDto>(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, productDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
    {
        try
        {
            if (updateProductDto is null)
                return BadRequest("Product is null");
            
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");
            
            var product = _repository.Product.GetProductById(id);
            if (product is null)
                return NotFound();
            
            _mapper.Map(updateProductDto, product);
            _repository.Product.UpdateProduct(product);
            _repository.Save();
            
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(Guid id)
    {
        try
        {
            var product = _repository.Product.GetProductById(id);
            if (product is null)
                return NotFound();
            
            _repository.Product.DeleteProduct(product);
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