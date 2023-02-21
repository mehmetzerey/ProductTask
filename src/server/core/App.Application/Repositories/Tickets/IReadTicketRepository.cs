using App.Application.ModelView.Ticket;

namespace App.Application.Repositories.Tickets;

public interface IReadTicketRepository : IReadRepository<Ticket>
{
    List<GetTicketViewModel> GetAll();
    List<GetTicketViewModel> GetByUserId(int userId);
    Ticket GetById(int id);
}
