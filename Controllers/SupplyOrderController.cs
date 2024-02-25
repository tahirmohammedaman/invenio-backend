using AutoMapper;
using invenio.Models.Dtos;
using invenio.Repositories;
using invenio.Services.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MimeKit;

namespace invenio.Controllers;

[ApiController]
[Authorize]
[Route("/api/supplyorders")]
public class SupplyOrderController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    private readonly IMailSender _mailSender;
    
    public SupplyOrderController(IRepositoryWrapper repository, IMapper mapper, IMailSender mailSender)
    {
        _repository = repository;
        _mapper = mapper;
        _mailSender = mailSender;
    }
    
    [HttpGet]
    [EnableQuery]
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
    [EnableQuery]
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
    [Authorize(Roles = "Admin")]
    public IActionResult CreateSupplyOrder([FromForm] CreateSupplyOrderDto createSupplyOrderDto)
    {
        try
        {
            var supplyOrder = _mapper.Map<Models.SupplyOrder>(createSupplyOrderDto);

            if (createSupplyOrderDto.OrderDate is null)
                supplyOrder.OrderDate = DateTime.Now.ToUniversalTime();

            if (createSupplyOrderDto.DeliveryDate is null)
            {
                var supply = _repository.Supply.GetSupplyById(supplyOrder.SupplyId);
                if (supply is not null && supply.SupplyLeadTime.HasValue && supply.SupplyLeadTime.Value > 0)
                {
                    supplyOrder.DeliveryDate = supplyOrder.OrderDate.AddDays(supply.SupplyLeadTime.Value).ToUniversalTime();
                }
            }
                
            _repository.SupplyOrder.CreateSupplyOrder(supplyOrder);
            _repository.Save();
            
            // Send mail
            var mail = new Mail(
                new [] {"tahiroam99@gmail.com"},
                "New Supply Order",
                "A new supply order has been created"
                );
            // AWAIT
            _mailSender.SendMail(mail);
            
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
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateSupplyOrder(Guid id, [FromForm] UpdateSupplyOrderDto updateSupplyOrderDto)
    {
        try
        {
            var supplyOrder = _repository.SupplyOrder.GetSupplyOrderById(id);
            if (supplyOrder is null)
                return NotFound();

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
    [Authorize(Roles = "Admin")]
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
    
    [HttpPost]
    [Route("{id}/delivery")]
    [Authorize(Roles = "Admin")]
    public IActionResult MarkSupplyOrderAsDeliveredAndUpdateStock (Guid id)
    {
        try
        {
            var supplyOrder = _repository.SupplyOrder.GetSupplyOrderById(id);
            if (supplyOrder is null)
                return NotFound();

            supplyOrder.DeliveryDate = DateTime.Now.ToUniversalTime();
            supplyOrder.IsDelivered = true;
            _repository.SupplyOrder.UpdateSupplyOrder(supplyOrder);
            
            // Update stock
            var stock = _repository.Stock.GetStockByProductIdAndWarehouseId(supplyOrder.Supply.ProductId, supplyOrder.WarehouseId);
            if (stock is not null)
            {
                stock.StockQuantity += supplyOrder.Quantity;
                _repository.Stock.UpdateStock(stock);
            }

            else
            {
                stock = new Models.Stock
                {
                    ProductId = supplyOrder.Supply.ProductId,
                    WarehouseId = supplyOrder.WarehouseId,
                    StockQuantity = supplyOrder.Quantity,
                    LowStockThreshold = 0,
                };
                _repository.Stock.CreateStock(stock);
            }
            _repository.Save();

            // Send mail
            var mail = new Mail(
                new [] {"tahiroam99@gmail.com"},
                "Supply order delivery",
                "Supply order has been delivered"
            );
            // AWAIT
            _mailSender.SendMail(mail);
            
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

}