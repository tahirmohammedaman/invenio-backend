using invenio.Models;
using invenio.Services;
using Microsoft.AspNetCore.Mvc;

namespace invenio.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;
    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Product> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _service.GetById(id);

        if (product is null)
            return NotFound();

        return product;
    }

    [HttpPost]
    public IActionResult Add(Product newProduct)
    {
        Console.WriteLine(newProduct.ToString());
        var product = _service.Add(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        var existingProduct = _service.GetById(id);
        if (existingProduct is null)
            return NotFound();
        
        _service.Update(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _service.GetById(id);
        if (product is null)
            return NotFound();
        
        _service.Delete(id);
        return NoContent();
    }
}