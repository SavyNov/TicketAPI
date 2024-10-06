using AutoMapper;
using TicketAPI.Contracts.Ticket;
using TicketAPI.Contracts.User;
using TicketAPI.Data;
using TicketAPI.Utils;

namespace TicketAPI.Repository.UserRepository {
    public class UserRepository : IUserRepository{

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRepository (DataContext datacontext, IMapper mapper) {
            _dataContext=datacontext;
            _mapper=mapper;
        }

        public async Task<List<ViewTicketDTO>> CheckTickets(int userId, CancellationToken cancellationToken) {
            var tickets = _dataContext.Tickets.Where(x => x.User.Id==userId).ToList();
            return _mapper.Map<List<ViewTicketDTO>>(tickets);
            
        }
            

        public async Task<OperationResult<ViewUserDTO>> DeleteUser(int id, CancellationToken cancellationToken) {
            var user = await _dataContext.Users.FindAsync(id);
            if (user is null)
                return OperationResult<ViewUserDTO>.Failure("No user found");
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return OperationResult<ViewUserDTO>.Success("User deleted succesfully"); ;
        }

        public async Task<List<ViewUserDTO>> GetAllUsers(CancellationToken cancellationToken) {
            var users =  await _dataContext.Users.ToListAsync();
            return _mapper.Map<List<ViewUserDTO>>(users);
        }

        public async Task<OperationResult<ViewUserDTO>> GetUser(int id, CancellationToken cancellationToken) {
            var user = await _dataContext.Users.FindAsync(id);
            var userDTO = _mapper.Map<ViewUserDTO>(user);
            if (user is null)
                return OperationResult<ViewUserDTO>.Failure(userDTO, "No user found");
            return OperationResult<ViewUserDTO>.Success(userDTO, "User found"); ;
        }

        public async Task<OperationResult<ViewUserDTO>> Login(LoginUserDTO user, CancellationToken cancellationToken) {
            var check = await _dataContext.Users.Where(x => x.Email==user.Email).SingleOrDefaultAsync();
            if (check is not null) {
                if (check.Password==user.Password) {
                    var viewUser = _mapper.Map<ViewUserDTO>(check);
                    return OperationResult<ViewUserDTO>.Success(viewUser, "Login succesfully");
                }
                return OperationResult<ViewUserDTO>.Failure("Wrong password for user");
            }
            return OperationResult<ViewUserDTO>.Failure("No user with such email");
        }

        public async Task<OperationResult<ViewUserDTO>> Register(InputUserDTO user, CancellationToken cancellationToken) {
            var check = await _dataContext.Users.Where(x => x.Email==user.Email).SingleOrDefaultAsync();
            if (check is null) {
                var add = _mapper.Map<User>(user);
                _dataContext.Add(add);
                await _dataContext.SaveChangesAsync(cancellationToken);
                var viewUser = _mapper.Map<ViewUserDTO>(add);
                return OperationResult<ViewUserDTO>.Success(viewUser, "Registration succesfully");
            }
            return OperationResult<ViewUserDTO>.Failure("Email already in use");
        }
        public async Task<OperationResult<ViewUserDTO>> UpdateUser(int id, UpdateUserDTO user, CancellationToken cancellationToken) {
            var dbUser = _dataContext.Users.Find(id);
            if (dbUser!=null) {
                _mapper.Map(user, dbUser);
                await _dataContext.SaveChangesAsync();              
                return OperationResult<ViewUserDTO>.Success("User changed succesfully");     
            }
            return OperationResult<ViewUserDTO>.Failure("User not found");   
        }
    }
}
