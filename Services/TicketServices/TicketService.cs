using AutoMapper;
using TicketAPI.Contracts.Ticket;
using TicketAPI.Data;
using TicketAPI.Repository.TicketRepository;
using TicketAPI.Utils;

namespace TicketAPI.Services.TicketServices {
    public class TicketService: ITicketService {

        private readonly ITicketRepository _ticketRepository;
        private readonly IConfiguration _configuration;


        public TicketService (ITicketRepository ticketRepository, IConfiguration configuration, IMapper mapper, DataContext dataContext) {
            _ticketRepository=ticketRepository;
            _configuration=configuration;
        }

        public async Task<OperationResult<ViewTicketDTO>> Buy(InputTicketDTO ticket, int userId, CancellationToken cancellationToken) {
            var repoResult = await _ticketRepository.Buy(ticket, userId, cancellationToken);
            var subject = _configuration.GetValue<string>("TicketData:BoughtSubject");
            var message = _configuration.GetValue<string>("TicketData:BoughtMessage");
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewTicketDTO>.Failure(repoResult.Message);
            }
            return OperationResult<ViewTicketDTO>.Success("Ticket bought");
        }


            public async Task<OperationResult<ViewTicketDTO>> DeleteTicket(int id, CancellationToken cancellationToken) {
            var repoResult = await _ticketRepository.DeleteTicket(id, cancellationToken);
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewTicketDTO>.Failure(repoResult.Message);
            }

            return OperationResult<ViewTicketDTO>.Success("Ticket deleted");
        }

        public async Task<List<ViewTicketDTO>> GetAllTickets(CancellationToken cancellationToken) {
            return await _ticketRepository.GetAllTickets(cancellationToken);
        }

        public async Task<OperationResult<ViewTicketDTO>> GetTicket(int id, CancellationToken cancellationToken) {
            var repoResult = await _ticketRepository.GetTicket(id, cancellationToken);
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewTicketDTO>.Failure(repoResult.Message);
            }

            return OperationResult<ViewTicketDTO>.Success(repoResult.Entity, "Ticket found");
        }

        public async Task<OperationResult<ViewTicketDTO>> UpdateTicket(int id, InputTicketDTO ticket, CancellationToken cancellationToken) {
            var repoResult = await _ticketRepository.UpdateTicket(id,ticket, cancellationToken);
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewTicketDTO>.Failure(repoResult.Message);
            }

            return OperationResult<ViewTicketDTO>.Success("Ticket updated");
        }
    }
}
