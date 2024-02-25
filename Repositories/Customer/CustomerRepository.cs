using invenio.Data;
using Microsoft.EntityFrameworkCore;

namespace invenio.Repositories.Customer;

public class CustomerRepository : RepositoryBase<Models.Customer>, ICustomerRepository
{
    public CustomerRepository(InvenioContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Models.Customer>> GetAllCustomers() =>
        await FindAll()
            .OrderBy(customer => customer.Name)
            .Include(customer => customer.SaleOrders) // Include SaleOrders relation
            .ThenInclude(saleOrder => saleOrder.Product) // Include Product relation within SaleOrders
            .ToListAsync();


    public async Task<Models.Customer?> GetCustomerById(Guid id) =>
        await FindByCondition(customer => customer.CustomerId.Equals(id))
            .FirstOrDefaultAsync();

    public void CreateCustomer(Models.Customer customer) => Create(customer);

    public void UpdateCustomer(Models.Customer customer) => Context.Entry(customer).State = EntityState.Modified;

    public void DeleteCustomer(Models.Customer customer) => Delete(customer);
}