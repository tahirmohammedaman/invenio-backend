using invenio.Data;
using invenio.Models;
using Microsoft.EntityFrameworkCore;

namespace invenio.Services;

public class ProductService
{
    private readonly InvenioContext _context;

    public ProductService(InvenioContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products
            .AsNoTracking()
            .ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products
            .AsNoTracking()
            // .Include(product => product.Category) // Join
            .SingleOrDefault(product => product.Id == id);
    }

    public Product Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();

        return product;
    }

    public Product Update(Product product)
    {
        _context.Products.Update(product);
        // _context.Entry(product).CurrentValues.SetValues(product); // Partial update

        _context.SaveChanges();

        return product;
    }

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product is null)
            return;

        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}