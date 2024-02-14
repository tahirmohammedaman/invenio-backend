using Microsoft.AspNetCore.Mvc;
using invenio.Repositories.SaleOrder;
using invenio.Models.Dtos;
using AutoMapper;
using invenio.Models;
using invenio.Repositories;

namespace invenio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleOrderController : ControllerBase
    {
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

    // public SupplierController(IRepositoryWrapper repository, IMapper mapper)
    // {
    //     _repository = repository;
    //     _mapper = mapper;
    // }
        public SaleOrderController(IRepositoryWrapper repository, ISaleOrderRepository saleOrderRepository, IMapper mapper)
        {
            repository = _repository;
            _saleOrderRepository = saleOrderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSaleOrders()
        {
            var saleOrders = _saleOrderRepository.GetAllSaleOrders();
            var saleOrderDtos = _mapper.Map<IEnumerable<SaleOrderDto>>(saleOrders);
            return Ok(saleOrderDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleOrder(Guid id)
        {
            var saleOrder = _saleOrderRepository.GetSaleOrderById(id);
            if (saleOrder == null)
                return NotFound();

            var saleOrderDto = _mapper.Map<SaleOrderDto>(saleOrder);
            return Ok(saleOrderDto);
        }

        [HttpPost]
        public IActionResult CreateSaleOrder(CreateSaleOrderDto saleOrderDto)
        {
            var saleOrder = _mapper.Map<SaleOrder>(saleOrderDto);
            _saleOrderRepository.CreateSaleOrder(saleOrder);
            _repository.Save();

            var createdSaleOrderDto = _mapper.Map<SaleOrderDto>(saleOrder);
            return CreatedAtAction(nameof(GetSaleOrder), new { id = createdSaleOrderDto.ProductId }, createdSaleOrderDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleOrder(Guid id, UpdateSaleOrderDto saleOrderDto)
        {
            // var existingSaleOrder = _saleOrderRepository.FindById(id);
            var existingSaleOrder = _saleOrderRepository.GetSaleOrderById(id);
            if (existingSaleOrder == null)
                return NotFound();

            _mapper.Map(saleOrderDto, existingSaleOrder);
            _saleOrderRepository.UpdateSaleOrder(existingSaleOrder);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrder(Guid id)
        {
            var existingSaleOrder = _saleOrderRepository.GetSaleOrderById(id);
            if (existingSaleOrder == null)
                return NotFound();

            _saleOrderRepository.DeleteSaleOrder(existingSaleOrder);
            _repository.Save();

            return NoContent();
        }
    }
}
