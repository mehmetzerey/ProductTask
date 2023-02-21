using App.Application.ModelView.Ticket;
using App.Application.Repositories.Tickets;

namespace App.Persistence.Repositories.Tickets;

public class ReadTicketRepository : ReadRepository<Ticket>, IReadTicketRepository
{
    public ReadTicketRepository(AppIdentityDbContext dbContext) : base(dbContext)
    {
    }

    public List<GetTicketViewModel> GetAll()
    {
        return base.GetAll().Select(x => new GetTicketViewModel
        {
            Created = x.CreatedDate,
            CustomerFullName = x.Customer.Name + " " + x.Customer.Surname,
            Id = x.Id,
            Message = x.Message,
            Status = TicketStatus.From(x.Status).Name,
            Subject = x.Subject,
            StatusId = x.Status
        }).ToList();
    }

    public Ticket GetById(int id)
    {
        return base.GetWhere(x => x.Id == id, true, y => y.Customer).FirstOrDefault();
    }
}
