using App.Application.ModelView.Product;

namespace App.Application.Repositories.Products;

public interface IReadProductRepository : IReadRepository<Product>
{
    List<GetProductViewModel> GetAll();
    Product GetById(int id);
}
