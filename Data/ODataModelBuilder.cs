using invenio.Models.Dtos;
using invenio.Models.Dtos.Category;
using invenio.Models.Dtos.Product;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace invenio.Data;

public static class ODataModelBuilder
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        builder.EntitySet<SupplierDto>("Suppliers");
        builder.EntitySet<CategoryDto>("Categories");
        builder.EntitySet<ProductDto>("Products");
        builder.EntitySet<SupplyDto>("Supplies");
        builder.EntitySet<StockDto>("Stocks");
        builder.EntitySet<WarehouseDto>("Warehouses");
        builder.EntitySet<SupplyOrderDto>("SupplyOrders");
        builder.EntitySet<CustomerDto>("Customers");
        builder.EntitySet<SaleOrderDto>("SaleOrders");

        builder.EntitySet<UserDto>("Users");
        return builder.GetEdmModel();
    }
}