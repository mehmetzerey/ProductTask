namespace App.Application.ModelView.Product;

public class GetProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string ImageURL { get; set; }
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
}
