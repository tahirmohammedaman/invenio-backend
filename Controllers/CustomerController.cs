using AutoMapper;
using invenio.Models;
using invenio.Models.Dtos;
using invenio.Repositories;
using invenio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace invenio.Controllers;

[ApiController]
[Authorize]
[Route("/api/customers")]
public class CustomerController : ODataController
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    
    public CustomerController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
    {
        try
        {
            var customers = await _repository.Customer.GetAllCustomers();
            var customersResponse = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            
            return Ok(customersResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    [EnableQuery]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(Guid id)
    {
        try
        {
            var customer = await _repository.Customer.GetCustomerById(id);

            if (customer is null)
                return NotFound();

            var customerResponse = _mapper.Map<CustomerDto>(customer);
            return Ok(customerResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CustomerDto>> CreateCustomer([FromForm] CreateCustomerDto createCustomerDto)
    {
        try
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            if (createCustomerDto.Logo is not null) 
                customer.LogoPath = FileService.UploadFile(createCustomerDto.Logo);
            
            _repository.Customer.CreateCustomer(customer);
            await _repository.SaveAsync();

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customerDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CustomerDto>> UpdateCustomer(Guid id, [FromForm] UpdateCustomerDto updateCustomerDto)
    {
        try
        {
            var customer = await _repository.Customer.GetCustomerById(id);
            if (customer is null)
                return NotFound();
            
            _mapper.Map(updateCustomerDto, customer);
            if (updateCustomerDto.Logo is not null)
            {
                if (customer.LogoPath is not null)
                    FileService.DeleteFile(customer.LogoPath);
                customer.LogoPath = FileService.UploadFile(updateCustomerDto.Logo);
            }
            
            _repository.Customer.UpdateCustomer(customer);
            await _repository.SaveAsync();

            var updatedCustomer = _mapper.Map<CustomerDto>(customer);
            return Ok(updatedCustomer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        try
        {
            var customer = await _repository.Customer.GetCustomerById(id);
            if (customer is null)
                return NotFound();
            
            if (customer.LogoPath is not null)
                FileService.DeleteFile(customer.LogoPath);
            
            _repository.Customer.DeleteCustomer(customer);
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