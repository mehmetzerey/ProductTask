using App.Application.ModelView.Ticket;

namespace App.Application.Repositories.Tickets;

public interface IWriteTicketRepository : IWriteRepository<Ticket>
{
    Task<ServiceResponse> Create(CreateTicketViewModel model, int customerId);
    Task<ServiceResponse> UpdateStatus(Ticket model);
}
