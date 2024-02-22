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
    public ActionResult<IEnumerable<SaleOrderDto>> GetAll()
    {
        try
        {
            var saleOrders = _repository.SaleOrder.GetAllSaleOrders();
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
    public ActionResult<SaleOrderDto> GetSaleOrderById(Guid id)
    {
        try
        {
            var saleOrder = _repository.SaleOrder.GetSaleOrderById(id);

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
    public ActionResult<SaleOrderDto> CreateSaleOrder([FromForm] CreateSaleOrderDto createSaleOrderDto)
    {
        try
        {
            var saleOrder = _mapper.Map<Models.SaleOrder>(createSaleOrderDto);
            
            if (createSaleOrderDto.OrderDate is null)
                saleOrder.OrderDate = DateTime.Now.ToUniversalTime();
            
            _repository.SaleOrder.CreateSaleOrder(saleOrder);
            _repository.Save();
            
            var saleOrderResponse = _mapper.Map<SaleOrderDto>(saleOrder);
            return Ok(saleOrderResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPut("{id}")]
    public ActionResult<SaleOrderDto> UpdateSaleOrder(Guid id, [FromForm] UpdateSaleOrderDto updateSaleOrderDto)
    {
        try
        {
            var saleOrder = _repository.SaleOrder.GetSaleOrderById(id);

            if (saleOrder is null)
                return NotFound();

            _mapper.Map(updateSaleOrderDto, saleOrder);
            _repository.SaleOrder.UpdateSaleOrder(saleOrder);
            _repository.Save();
            
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
    public IActionResult DeleteSaleOrder(Guid id)
    {
        try
        {
            var saleOrder = _repository.SaleOrder.GetSaleOrderById(id);

            if (saleOrder is null)
                return NotFound();

            _repository.SaleOrder.DeleteSaleOrder(saleOrder);
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