using TicketAPI.Contracts.Ticket;
using TicketAPI.Utils;

namespace TicketAPI.Services.TicketServices {
    public interface ITicketService {
        Task<List<ViewTicketDTO>> GetAllTickets(CancellationToken cancellationToken);
        Task<OperationResult<ViewTicketDTO>> GetTicket(int id, CancellationToken cancellationToken);
        Task<OperationResult<ViewTicketDTO>> DeleteTicket(int id, CancellationToken cancellationToken);
        Task<OperationResult<ViewTicketDTO>> UpdateTicket(int id, InputTicketDTO ticket , CancellationToken cancellationToken);
        Task<OperationResult<ViewTicketDTO>> Buy(InputTicketDTO ticket, int userId, CancellationToken cancellationToken);
    }
}
