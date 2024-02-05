using AutoMapper;
using invenio.Models.Dtos;
using invenio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace invenio.Controllers;

[ApiController]
[Route("/api/suppliers")]
public class SupplierController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public SupplierController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<SupplierDto>> GetAll()
    {
        try
        {
            var suppliers = _repository.Supplier.GetAllSuppliers();
            var suppliersResponse = _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
            
            return Ok(suppliersResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    public ActionResult<SupplierDto> GetSupplierById(Guid id)
    {
        try
        {
            var supplier = _repository.Supplier.GetSupplierById(id);

            if (supplier is null)
                return NotFound();

            var supplierResponse = _mapper.Map<SupplierDto>(supplier);
            return Ok(supplierResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    public IActionResult CreateSupplier([FromForm] CreateSupplierDto createSupplierDto)
    {
        try
        {
            var supplier = _mapper.Map<Models.Supplier>(createSupplierDto);
            _repository.Supplier.CreateSupplier(supplier);
            _repository.Save();

            var supplierDto = _mapper.Map<SupplierDto>(supplier);
            
            return CreatedAtAction(nameof(GetSupplierById), new {id = supplier.SupplierId}, supplierDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateSupplier(Guid id, [FromForm] UpdateSupplierDto supplierDto)
    {
        try
        {
            var supplier = _repository.Supplier.GetSupplierById(id);
            if (supplier is null)
                return NotFound();

            _mapper.Map(supplierDto, supplier);
            _repository.Supplier.UpdateSupplier(supplier);
            _repository.Save();

            var supplierResponse = _mapper.Map<SupplierDto>(supplier);
            return Ok(supplierResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteSupplier(Guid id)
    {
        try
        {
            var supplier = _repository.Supplier.GetSupplierById(id);
            if (supplier is null)
                return NotFound();
            
            _repository.Supplier.DeleteSupplier(supplier);
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