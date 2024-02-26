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
[Route("/api/stocks")]
public class StockController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;

    public StockController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<StockDto>>> GetAll()
    {
        try
        {
            var stocks = await _repository.Stock.GetAllStocks();
            var stocksResponse = _mapper.Map<IEnumerable<StockDto>>(stocks);

            return Ok(stocksResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    [EnableQuery]
    public async Task<ActionResult<StockDto>> GetStockById(Guid id)
    {
        try
        {
            var stock = await _repository.Stock.GetStockById(id);

            if (stock is null)
                return NotFound();

            var stockResponse = _mapper.Map<StockDto>(stock);
            return Ok(stockResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateStock([FromForm] CreateStockDto createStockDto)
    {
        try
        {
            var stock = _mapper.Map<Models.Stock>(createStockDto);
            _repository.Stock.CreateStock(stock);
            await _repository.SaveAsync();

            var stockResponse = _mapper.Map<StockDto>(stock);
            
            return CreatedAtAction(nameof(GetStockById), new { id = stock.StockId }, stockResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStock(Guid id, [FromForm] UpdateStockDto updateStockDto)
    {
        try
        {
            var stock = await _repository.Stock.GetStockById(id);
            if (stock is null)
                return NotFound();

            _mapper.Map(updateStockDto, stock);
            _repository.Stock.UpdateStock(stock);
            await _repository.SaveAsync();

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
    public async Task<IActionResult> DeleteStock(Guid id)
    {
        try
        {
            var stock = await _repository.Stock.GetStockById(id);
            if (stock is null)
                return NotFound();

            _repository.Stock.DeleteStock(stock);
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