namespace App.Application.ModelView.Ticket;

public class GetTicketViewModel
{
    public int Id { get; set; }
    public string CustomerFullName { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }
    public int StatusId { get; set; }
    public DateTime Created { get; set; }
}
