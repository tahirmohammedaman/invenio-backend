using AutoMapper;
using invenio.Models.Dtos;
using invenio.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace invenio.Controllers;

[ApiController]
[Authorize]
[Route("/api/saleorders")]
public class SaleOrderController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public SaleOrderController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<SaleOrderDto>>> GetAll()
    {
        try
        {
            var saleOrders = await _repository.SaleOrder.GetAllSaleOrders();
            var saleOrdersResponse = _mapper.Map<IEnumerable<SaleOrderDto>>(saleOrders);
            
            return Ok(saleOrdersResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    [EnableQuery]
    public async Task<ActionResult<SaleOrderDto>> GetSaleOrderById(Guid id)
    {
        try
        {
            var saleOrder = await _repository.SaleOrder.GetSaleOrderById(id);

            if (saleOrder is null)
                return NotFound();

            var saleOrderResponse = _mapper.Map<SaleOrderDto>(saleOrder);
            return Ok(saleOrderResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<SaleOrderDto>> CreateSaleOrder([FromForm] CreateSaleOrderDto createSaleOrderDto)
    {
        try
        {
            var saleOrder = _mapper.Map<Models.SaleOrder>(createSaleOrderDto);
            
            if (createSaleOrderDto.OrderDate is null)
                saleOrder.OrderDate = DateTime.Now.ToUniversalTime();
            
            _repository.SaleOrder.CreateSaleOrder(saleOrder);
            await _repository.SaveAsync();
            
            var saleOrderResponse = _mapper.Map<SaleOrderDto>(saleOrder);
            return Ok(saleOrderResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<SaleOrderDto>> UpdateSaleOrder(Guid id, [FromForm] UpdateSaleOrderDto updateSaleOrderDto)
    {
        try
        {
            var saleOrder = await _repository.SaleOrder.GetSaleOrderById(id);

            if (saleOrder is null)
                return NotFound();

            _mapper.Map(updateSaleOrderDto, saleOrder);
            _repository.SaleOrder.UpdateSaleOrder(saleOrder);
            await _repository.SaveAsync();
            
            var saleOrderResponse = _mapper.Map<SaleOrderDto>(saleOrder);
            return Ok(saleOrderResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSaleOrder(Guid id)
    {
        try
        {
            var saleOrder = await _repository.SaleOrder.GetSaleOrderById(id);

            if (saleOrder is null)
                return NotFound();

            _repository.SaleOrder.DeleteSaleOrder(saleOrder);
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