using Microsoft.AspNetCore.Http;

namespace App.Application.ModelView.Product;

public class PutProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int Price { get; set; }
    public IFormFile? FileLink { get; set; }
}
