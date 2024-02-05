using AutoMapper;
using invenio.Models.Dtos;
using invenio.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace invenio.Controllers;

[ApiController]
[Route("/api/stocks")]
public class StockController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;

    public StockController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<StockDto>> GetAll()
    {
        try
        {
            var stocks = _repository.Stock.GetAllStocks();
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
    public ActionResult<StockDto> GetStockById(Guid id)
    {
        try
        {
            var stock = _repository.Stock.GetStockById(id);

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
    public IActionResult CreateStock([FromForm] CreateStockDto createStockDto)
    {
        try
        {
            var stock = _mapper.Map<Models.Stock>(createStockDto);
            _repository.Stock.CreateStock(stock);
            _repository.Save();

            var stockResponse = _mapper.Map<StockDto>(stock);
            
            return CreatedAtAction(nameof(GetStockById), new { id = stock.StockId }, stockResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateStock(Guid id, [FromForm] UpdateStockDto updateStockDto)
    {
        try
        {
            var stock = _repository.Stock.GetStockById(id);
            if (stock is null)
                return NotFound();

            _mapper.Map(updateStockDto, stock);
            _repository.Stock.UpdateStock(stock);
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
    public IActionResult DeleteStock(Guid id)
    {
        try
        {
            var stock = _repository.Stock.GetStockById(id);
            if (stock is null)
                return NotFound();

            _repository.Stock.DeleteStock(stock);
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