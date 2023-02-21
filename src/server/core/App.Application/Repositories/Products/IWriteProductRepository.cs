using App.Application.ModelView.Product;

namespace App.Application.Repositories.Products;

public interface IWriteProductRepository : IWriteRepository<Product>
{
    Task<ServiceResponse> Create(Product model);
    Task<ServiceResponse> UpdateProduct(Product model);
    Task<ServiceResponse> DeleteProduct(Product model);
}
