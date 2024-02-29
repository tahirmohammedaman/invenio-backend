using System.Reflection;
using AutoMapper;
using invenio.Models;
using invenio.Models.Dtos.Product;
using invenio.Repositories;
using invenio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace invenio.Controllers;

[ApiController]
[Authorize]
[Route("/api/products")]
public class ProductController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public ProductController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        try
        {
            var products = await _repository.Product.GetAllProducts();
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
    [EnableQuery]
    public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
    {
        try
        {
            var product = await _repository.Product.GetProductById(id);

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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
    {
        try
        {
            var product = _mapper.Map<Product>(createProductDto);
            
            createProductDto.Images.ForEach(image =>
            {
                product.ImagePaths.Add(FileService.UploadFile(image));
            });

            _repository.Product.CreateProduct(product);
            await _repository.SaveAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, productDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] UpdateProductDto updateProductDto)
    {
        try
        {
            var product = await _repository.Product.GetProductById(id);
            if (product is null)
                return NotFound();

            _mapper.Map(updateProductDto, product);

            if (updateProductDto.Images is not null)
            {
                product.ImagePaths.ForEach(image => FileService.DeleteFile(image));
                product.ImagePaths.Clear();
                updateProductDto.Images.ForEach(image => 
                    product.ImagePaths.Add(FileService.UploadFile(image))
                    );
            }

            _repository.Product.UpdateProduct(product);
            await _repository.SaveAsync();

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var product = await _repository.Product.GetProductById(id);
            if (product is null)
                return NotFound();

            product.ImagePaths.ForEach(image => FileService.DeleteFile(image));
            _repository.Product.DeleteProduct(product);
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