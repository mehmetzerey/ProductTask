namespace App.Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string ImageURL { get; set; }
    public decimal Price { get; set; }
}
