using Microsoft.AspNetCore.Http;

namespace App.Application.ModelView.Product;

public class CreateProductViewModel
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int Price { get; set; }
    public IFormFile FileLink { get; set; }
}
