using AutoMapper;
using invenio.Models;
using invenio.Services;
using invenio.Models.Dtos;
using invenio.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace invenio.Controllers;

[ApiController]
[Authorize]
[Route("/api/suppliers")]
public class SupplierController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;

    public SupplierController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
    {
        try
        {
            var suppliers = await _repository.Supplier.GetAllSuppliers();
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
    [EnableQuery]
    public async Task<ActionResult<SupplierDto>> GetSupplierById(Guid id)
    {
        try
        {
            var supplier = await _repository.Supplier.GetSupplierById(id);

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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateSupplier([FromForm] CreateSupplierDto createSupplierDto)
    {
        try
        {
            var supplier = _mapper.Map<Models.Supplier>(createSupplierDto);
            if (createSupplierDto.Logo is not null)
                supplier.LogoPath = FileService.UploadFile(createSupplierDto.Logo);

            _repository.Supplier.CreateSupplier(supplier);
            await _repository.SaveAsync();

            var supplierDto = _mapper.Map<SupplierDto>(supplier);

            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.SupplierId }, supplierDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateSupplier(Guid id, [FromForm] UpdateSupplierDto updateSupplierDto)
    {
        try
        {
            var supplier = await _repository.Supplier.GetSupplierById(id);
            if (supplier is null)
                return NotFound();

            _mapper.Map(updateSupplierDto, supplier);
            if (updateSupplierDto.Logo is not null)
            {
                if (supplier.LogoPath is not null)
                    FileService.DeleteFile(supplier.LogoPath);
                supplier.LogoPath = FileService.UploadFile(updateSupplierDto.Logo);
            }

            _repository.Supplier.UpdateSupplier(supplier);
            await _repository.SaveAsync();

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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSupplier(Guid id)
    {
        try
        {
            var supplier = await _repository.Supplier.GetSupplierById(id);
            if (supplier is null)
                return NotFound();

            if (supplier.LogoPath is not null)
                FileService.DeleteFile(supplier.LogoPath);

            _repository.Supplier.DeleteSupplier(supplier);
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