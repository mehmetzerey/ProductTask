using App.Application.ModelView.Product;
using App.Application.Repositories.Products;

namespace App.Persistence.Repositories.Products;

public class ReadProductRepository : ReadRepository<Product>, IReadProductRepository
{
    public ReadProductRepository(AppIdentityDbContext context) : base(context)
    {
    }

    public List<GetProductViewModel> GetAll()
    {
        return base.GetAll().Select(x=> new GetProductViewModel
        {
            Code = x.Code,
            Created = x.CreatedDate,
            Id = x.Id,
            ImageURL = x.ImageURL,
            Name = x.Name,
            Price = x.Price
        }).ToList();
    }

    public Product GetById(int id)
    {
        return base.GetWhere(x => x.Id == id).FirstOrDefault();
    }
}
