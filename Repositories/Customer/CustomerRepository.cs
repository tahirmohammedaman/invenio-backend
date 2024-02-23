using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Customer;

public class CustomerRepository : RepositoryBase<Models.Customer>, ICustomerRepository
{
    public CustomerRepository(InvenioContext context) : base(context)
    {
    }

    public IEnumerable<Models.Customer> GetAllCustomers() =>
        FindAll()
            .OrderBy(customer => customer.Name)
            .Include(customer => customer.SaleOrders) // Include SaleOrders relation
            .ThenInclude(saleOrder => saleOrder.Product) // Include Product relation within SaleOrders
            .ToList();


    public Models.Customer? GetCustomerById(Guid id) =>
        FindByCondition(customer => customer.CustomerId.Equals(id))
            .FirstOrDefault();

    public void CreateCustomer(Models.Customer customer) => Create(customer);

    public void UpdateCustomer(Models.Customer customer) => Context.Entry(customer).State = EntityState.Modified;

    public void DeleteCustomer(Models.Customer customer) => Delete(customer);
}