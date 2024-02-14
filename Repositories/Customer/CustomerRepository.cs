using invenio.Data;
using invenio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace invenio.Repositories.Customer;
public class CustomerRepository : RepositoryBase<Models.Customer>, ICustomerRepository
{
    public CustomerRepository(InvenioContext _context) : base(_context) {}

    public IEnumerable<Models.Customer> GetAllCustomers()
    {
        return FindAll()
          .ToList();
    }

    public Models.Customer CustomerFindById(Guid id)
    {
      return FindByCondition(x => x.Id.Equals(id))
        .FirstOrDefault();
    }

    public void Create(Models.Customer customer)
    {
      Create(customer);
    }

    public void Update(Models.Customer customer)
    {
       Context.Entry(customer).State = EntityState.Modified;
    }

    public void Delete(Models.Customer customer)
    {
      Delete(customer);
    }
}
