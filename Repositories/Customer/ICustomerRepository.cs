using invenio.Models;

namespace invenio.Repositories.Customer
{
    public interface ICustomerRepository : IRepositoryBase<Models.Customer> {
      IEnumerable<Models.Customer> GetAllCustomers();
      Models.Customer? CustomerFindById(Guid id);

  // IEnumerable<Models.SaleOrder> GetAllSaleOrders();
  // Models.SaleOrder? GetSaleOrderById(Guid id);
  // void CreateSaleOrder(Models.SaleOrder saleOrder);
  // void UpdateSaleOrder(Models.SaleOrder saleOrder);
  // void DeleteSaleOrder(Models.SaleOrder saleOrder);
    }
}
