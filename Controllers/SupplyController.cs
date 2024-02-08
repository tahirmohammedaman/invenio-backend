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
[Authorize]
[Route("/api/supplies")]
public class SupplyController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public SupplyController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [EnableQuery]
    public ActionResult<IEnumerable<SupplyDto>> GetAll()
    {
        try
        {
            var supplies = _repository.Supply.GetAllSupplies();
            var suppliesResponse = _mapper.Map<IEnumerable<SupplyDto>>(supplies);
            
            return Ok(suppliesResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    [EnableQuery]
    public ActionResult<SupplyDto> GetSupplyById(Guid id)
    {
        try
        {
            var supply = _repository.Supply.GetSupplyById(id);

            if (supply is null)
                return NotFound();

            var supplyResponse = _mapper.Map<SupplyDto>(supply);
            return Ok(supplyResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult CreateSupply([FromForm] CreateSupplyDto createSupplyDto)
    {
        try
        {
            var supply = _mapper.Map<Supply>(createSupplyDto);
            _repository.Supply.CreateSupply(supply);
            _repository.Save();

            var supplyDto = _mapper.Map<SupplyDto>(supply);
            return CreatedAtAction(nameof(GetSupplyById), new { id = supply.SupplyId }, supplyDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateSupply(Guid id, [FromForm] UpdateSupplyDto updateSupplyDto)
    {
        try
        {
            var supply = _repository.Supply.GetSupplyById(id);

            if (supply is null)
                return NotFound();

            _mapper.Map(updateSupplyDto, supply);
            _repository.Supply.UpdateSupply(supply);
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
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteSupply(Guid id)
    {
        try
        {
            var supply = _repository.Supply.GetSupplyById(id);

            if (supply is null)
                return NotFound();

            _repository.Supply.DeleteSupply(supply);
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