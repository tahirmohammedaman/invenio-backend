using AutoMapper;
using invenio.Models;
using invenio.Models.Dtos;
using invenio.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace invenio.Controllers;

[ApiController]
// [Authorize]
[Route("/api/warehouses")]
public class WarehouseController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;

    public WarehouseController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [EnableQuery]
    public ActionResult<IEnumerable<WarehouseDto>> GetAll()
    {
        try
        {
            var warehouses = _repository.Warehouse.GetAllWarehouses();
            var warehousesResponse = _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);

            return Ok(warehousesResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<WarehouseDto> GetWarehouseById(Guid id)
    {
        try
        {
            var warehouse = _repository.Warehouse.GetWarehouseById(id);

            if (warehouse is null)
                return NotFound();

            var warehouseResponse = _mapper.Map<WarehouseDto>(warehouse);
            return Ok(warehouseResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public IActionResult CreateWarehouse([FromForm] CreateWarehouseDto createWarehouseDto)
    {
        try
        {
            var warehouse = _mapper.Map<Warehouse>(createWarehouseDto);
            _repository.Warehouse.CreateWarehouse(warehouse);
            _repository.Save();

            var warehouseDto = _mapper.Map<WarehouseDto>(warehouse);

            return CreatedAtAction(nameof(GetWarehouseById), new { id = warehouse.WarehouseId }, warehouseDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateWarehouse(Guid id, [FromForm] UpdateWarehouseDto updateWarehouseDto)
    {
        try
        {
            var warehouse = _repository.Warehouse.GetWarehouseById(id);
            if (warehouse is null)
                return NotFound();

            _mapper.Map(updateWarehouseDto, warehouse);
            _repository.Warehouse.UpdateWarehouse(warehouse);
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
    public IActionResult DeleteWarehouse(Guid id)
    {
        try
        {
            var warehouse = _repository.Warehouse.GetWarehouseById(id);
            if (warehouse is null)
                return NotFound();

            _repository.Warehouse.DeleteWarehouse(warehouse);
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