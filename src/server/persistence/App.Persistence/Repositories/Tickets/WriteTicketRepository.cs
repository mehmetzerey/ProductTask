using App.Application.ModelView.Ticket;
using App.Application.Repositories.Tickets;

namespace App.Persistence.Repositories.Tickets;

public class WriteTicketRepository : WriteRepository<Ticket>, IWriteTicketRepository
{
    public WriteTicketRepository(AppIdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ServiceResponse> Create(CreateTicketViewModel model, int customerId)
    {
        var response = new ServiceResponse();

        await base.AddAsync(new Ticket
        {
            CreatedDate= DateTime.Now,
            CustomerId = customerId,
            Message= model.Message,
            Status = TicketStatus.IslemYapilmadi.Id,
            Subject = model.Subject,
        });

        var result = await base.SaveAsync();

        response.IsSuccess = true;

        return response;
    }

    public async Task<ServiceResponse> UpdateStatus(Ticket model)
    {
        var response = new ServiceResponse();

        base.Update(model);

        await base.SaveAsync();

        response.IsSuccess = true;

        return response;
    }
}
