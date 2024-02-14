using Microsoft.AspNetCore.Mvc;
using invenio.Repositories.Customer;
using invenio.Models.Dtos;
using AutoMapper;
using invenio.Models;
using invenio.Repositories;

using invenio.Models.Dtos;

namespace invenio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public CustomerController(IRepositoryWrapper repository, ICustomerRepository customerRepository, IMapper mapper)
        {
            repository = _repository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(Guid id)
        {
            var customer = _customerRepository.CustomerFindById(id);
            if (customer == null)
                return NotFound();

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

        [HttpPost]
        public IActionResult CreateCustomer(CreateCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _customerRepository.Create(customer);
            _repository.Save();

            var createdCustomerDto = _mapper.Map<CustomerDto>(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomerDto.Id }, createdCustomerDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(Guid id, UpdateCustomerDto customerDto)
        {
            var existingCustomer = _customerRepository.CustomerFindById(id);
            if (existingCustomer == null)
                return NotFound();

            _mapper.Map(customerDto, existingCustomer);
            _customerRepository.Update(existingCustomer);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id)
        {
            var existingCustomer = _customerRepository.CustomerFindById(id);
            if (existingCustomer == null)
                return NotFound();

            _customerRepository.Delete(existingCustomer);
            _repository.Save();

            return NoContent();
        }
    }
}
