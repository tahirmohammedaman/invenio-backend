using AutoMapper;
using invenio.Models;
using invenio.Models.Dtos.Product;
using invenio.Repositories;
using invenio.Services;
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
    public IActionResult CreateProduct([FromForm] CreateProductDto createProductDto)
    {
        try
        {
            var product = _mapper.Map<Product>(createProductDto);

            var createImages = new[]
                { createProductDto.Image1, createProductDto.Image2, createProductDto.Image3, createProductDto.Image4 };
            var productImages = new[]
                { product.Image1Path, product.Image2Path, product.Image3Path, product.Image4Path };

            for (int i = 0; i < createImages.Length; i++)
                if (createImages[i] is not null)
                    productImages[i] = FileService.UploadFile(createImages[i]);

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
    public IActionResult UpdateProduct(Guid id, [FromForm] UpdateProductDto updateProductDto)
    {
        try
        {
            var product = _repository.Product.GetProductById(id);
            if (product is null)
                return NotFound();

            _mapper.Map(updateProductDto, product);

            var updateImages = new[]
                { updateProductDto.Image1, updateProductDto.Image2, updateProductDto.Image3, updateProductDto.Image4 };
            var productImages = new[]
                { product.Image1Path, product.Image2Path, product.Image3Path, product.Image4Path };

            for (int i = 0; i < updateImages.Length; i++)
            {
                if (updateImages[i] is not null)
                {
                    if (productImages[i] is not null)
                        FileService.DeleteFile(productImages[i]);
                    productImages[i] = FileService.UploadFile(updateImages[i]);
                }
            }

            (product.Image1Path, product.Image2Path, product.Image3Path, product.Image4Path) = (productImages[0],
                productImages[1], productImages[2], productImages[3]);


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