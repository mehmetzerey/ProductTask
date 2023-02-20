namespace App.Domain.Models;

public class Ticket : BaseEntity
{
    public int CustomerId { get; set; }
    public ApplicationUser Customer { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public int Status { get; set; }
    public DateTime Updated { get; set; }
}
