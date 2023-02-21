using App.Application.ModelView.Product;
using App.Application.ModelView.Ticket;
using App.Application.Repositories.Products;

namespace App.Persistence.Repositories.Products;

public class WriteProductRepository : WriteRepository<Product>, IWriteProductRepository
{
    public WriteProductRepository(AppIdentityDbContext context) : base(context)
    {
    }

    public async Task<ServiceResponse> Create(Product model)
    {
        var response = new ServiceResponse();

        await base.AddAsync(model);

        var result = await base.SaveAsync();

        response.IsSuccess = true;

        return response;
    }

    public async Task<ServiceResponse> DeleteProduct(Product model)
    {
        var response = new ServiceResponse();

        base.Remove(model);
        await base.SaveAsync();

        response.IsSuccess = true;

        return response;
    }

    public async Task<ServiceResponse> UpdateProduct(Product model)
    {
        var response = new ServiceResponse();

        base.Update(model);

        await base.SaveAsync();

        response.IsSuccess = true;

        return response;
    }
}
