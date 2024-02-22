namespace invenio.Repositories.Customer;

public interface ICustomerRepository
{
    IEnumerable<Models.Customer> GetAllCustomers();
    Models.Customer? GetCustomerById(Guid id);
    void CreateCustomer(Models.Customer customer);
    void UpdateCustomer(Models.Customer customer);
    void DeleteCustomer(Models.Customer customer);
}