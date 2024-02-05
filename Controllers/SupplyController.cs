using AutoMapper;
using invenio.Models;
using invenio.Models.Dtos;
using invenio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace invenio.Controllers;

[ApiController]
[Route("/api/supplies")]
public class SupplyController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public SupplyController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
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