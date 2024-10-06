using AutoMapper;
using TicketAPI.Contracts.Ticket;
using TicketAPI.Data;
using TicketAPI.Utils;

namespace TicketAPI.Repository.TicketRepository {
    public class TicketRepository : ITicketRepository{

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TicketRepository(DataContext dataContext, IMapper mapper) {
            _dataContext=dataContext;
            _mapper=mapper;
        }
        public async Task<List<ViewTicketDTO>> GetAllTickets(CancellationToken cancellationToken) {
            var ticket = await _dataContext.Tickets.ToListAsync(cancellationToken);
            
            return _mapper.Map<List<ViewTicketDTO>>(ticket);
        }

        public async Task<OperationResult<ViewTicketDTO>> GetTicket(int id, CancellationToken cancellationToken) {
            var ticket = await _dataContext.Tickets.FindAsync(id);
            var ticketDTO = _mapper.Map<ViewTicketDTO>(ticket);
            if (ticketDTO is null)
                return OperationResult<ViewTicketDTO>.Failure(ticketDTO, "Ticket not found");

            return OperationResult<ViewTicketDTO>.Success(ticketDTO, "Ticket found");
            
        }

        public async Task<OperationResult<ViewTicketDTO>> DeleteTicket(int id, CancellationToken cancellationToken) {
            var ticket = await _dataContext.Tickets.FindAsync(id);
            if (ticket is null)
                return OperationResult<ViewTicketDTO>.Failure("Ticket not found");

            _dataContext.Tickets.Remove(ticket);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return OperationResult<ViewTicketDTO>.Success("Ticket deleted");
        }

        public async Task<OperationResult<ViewTicketDTO>> Buy(InputTicketDTO ticket, int userId, CancellationToken cancellationToken) {
            var ticket_to_add = _mapper.Map<Ticket>(ticket);
            var user = await _dataContext.Users.Where(x => x.Id==userId).FirstOrDefaultAsync();
            if (user!=null && ticket!=null) {
                
                ticket.TicketName=user.Name;
                user.Tickets.Add(ticket_to_add);
                await _dataContext.SaveChangesAsync();
                return OperationResult<ViewTicketDTO>.Success("Ticket sucesfully bought");
            }
            return OperationResult<ViewTicketDTO>.Failure("Ticket not found");
        }

        public async Task<OperationResult<ViewTicketDTO>> UpdateTicket(int id, InputTicketDTO ticket, CancellationToken cancellationToken) {
            var dbTicket = await _dataContext.Tickets.FindAsync(id);
            if (dbTicket!=null) {
                _mapper.Map(ticket, dbTicket);
                await _dataContext.SaveChangesAsync();
                return OperationResult<ViewTicketDTO>.Success("Ticket changed succesfully");
            }
            return OperationResult<ViewTicketDTO>.Failure("Ticket not found");
        }
    }
}