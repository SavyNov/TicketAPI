using TicketAPI.Contracts.Ticket;
using TicketAPI.Contracts.User;
using TicketAPI.Utils;

namespace TicketAPI.Repository.UserRepository {
    public interface IUserRepository {
        Task<List<ViewUserDTO>> GetAllUsers(CancellationToken cancellationToken);
        Task<OperationResult<ViewUserDTO>> GetUser(int id, CancellationToken cancellationToken);
        Task<OperationResult<ViewUserDTO>> DeleteUser(int id, CancellationToken cancellationToken);
        Task<OperationResult<ViewUserDTO>> UpdateUser(int id, UpdateUserDTO user, CancellationToken cancellationToken);
        Task<OperationResult<ViewUserDTO>> Login(LoginUserDTO user, CancellationToken cancellationToken);
        Task<OperationResult<ViewUserDTO>> Register(InputUserDTO user, CancellationToken cancellationToken);
        Task<List<ViewTicketDTO>> CheckTickets(int userId, CancellationToken cancellationToken);
    }
}
