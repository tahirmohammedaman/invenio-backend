using AutoMapper;
using invenio.Models.Dtos;
using invenio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace invenio.Controllers;

[ApiController]
[Route("/api/supply-orders")]
public class SupplyOrderController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public SupplyOrderController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<SupplyOrderDto>> GetAll()
    {
        try
        {
            var supplyOrders = _repository.SupplyOrder.GetAllSupplyOrders();
            var supplyOrdersResponse = _mapper.Map<IEnumerable<SupplyOrderDto>>(supplyOrders);
            
            return Ok(supplyOrdersResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    public ActionResult<SupplyOrderDto> GetSupplyOrderById(Guid id)
    {
        try
        {
            var supplyOrder = _repository.SupplyOrder.GetSupplyOrderById(id);

            if (supplyOrder is null)
                return NotFound();

            var supplyOrderResponse = _mapper.Map<SupplyOrderDto>(supplyOrder);
            return Ok(supplyOrderResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    public IActionResult CreateSupplyOrder([FromBody] CreateSupplyOrderDto createSupplyOrderDto)
    {
        try
        {
            var supplyOrder = _mapper.Map<Models.SupplyOrder>(createSupplyOrderDto);
            _repository.SupplyOrder.CreateSupplyOrder(supplyOrder);
            _repository.Save();
            
            var supplyOrderDto = _mapper.Map<SupplyOrderDto>(supplyOrder);
            
            return CreatedAtAction(nameof(GetSupplyOrderById), new { id = supplyOrder.SupplyOrderId }, supplyOrderDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateSupplyOrder(Guid id, [FromBody] UpdateSupplyOrderDto updateSupplyOrderDto)
    {
        try
        {
            var supplyOrder = _repository.SupplyOrder.GetSupplyOrderById(id);
            if (supplyOrder is null)
                return NotFound();

            Console.WriteLine(supplyOrder.SupplyOrderId + " " + supplyOrder.SupplyId + " " + supplyOrder.WarehouseId + " " + supplyOrder.Quantity + " " + supplyOrder.Price + " " + supplyOrder.OrderDate + " " + supplyOrder.DeliveryDate + " " + supplyOrder.IsDelivered);
            Console.WriteLine(updateSupplyOrderDto.SupplyId + " " + updateSupplyOrderDto.WarehouseId + " " + updateSupplyOrderDto.Quantity + " " + updateSupplyOrderDto.Price + " " + updateSupplyOrderDto.OrderDate + " " + updateSupplyOrderDto.DeliveryDate + " " + updateSupplyOrderDto.IsDelivered);
            _mapper.Map(updateSupplyOrderDto, supplyOrder);
            _repository.SupplyOrder.UpdateSupplyOrder(supplyOrder);
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
    public IActionResult DeleteSupplyOrder(Guid id)
    {
        try
        {
            var supplyOrder = _repository.SupplyOrder.GetSupplyOrderById(id);

            if (supplyOrder is null)
                return NotFound();

            _repository.SupplyOrder.DeleteSupplyOrder(supplyOrder);
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