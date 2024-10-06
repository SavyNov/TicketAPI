using AutoMapper;
using TicketAPI.Contracts.Ticket;
using TicketAPI.Contracts.User;
using TicketAPI.Repository.UserRepository;
using TicketAPI.Utils;


namespace TicketAPI.Services.UserServices {
    public class UserService: IUserService {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper) {
            _userRepository=userRepository;
            _configuration=configuration;
        }


        public async Task<List<ViewTicketDTO>> CheckTickets(int userId, CancellationToken cancellationToken) {
            return await _userRepository.CheckTickets(userId, cancellationToken);
        }

        public async Task<OperationResult<ViewUserDTO>> DeleteUser(int id, CancellationToken cancellationToken) {
            var repoResult = await _userRepository.DeleteUser(id, cancellationToken);
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewUserDTO>.Failure(repoResult.Message);
            }
            return OperationResult<ViewUserDTO>.Success("User deleted");
        }

        public async Task<List<ViewUserDTO>> GetAllUsers(CancellationToken cancellationToken) {
            return await _userRepository.GetAllUsers(cancellationToken);
            //return _mapper.Map<List<ViewUserDTO>>(users);
        }

        public async Task<OperationResult<ViewUserDTO>> GetUser(int id, CancellationToken cancellationToken) {
            var repoResult = await _userRepository.GetUser(id, cancellationToken);
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewUserDTO>.Failure(repoResult.Message);
            }
            return OperationResult<ViewUserDTO>.Success(repoResult.Entity,"User found");
        }

        public async Task<OperationResult<ViewUserDTO>> Login(LoginUserDTO user, CancellationToken cancellationToken) {
            var repoResult = await _userRepository.Login(user, cancellationToken);
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewUserDTO>.Failure(repoResult.Message);
            }
            return OperationResult<ViewUserDTO>.Success(repoResult.Entity, "Successfully log in");
        }

        public async Task<OperationResult<ViewUserDTO>> Register(InputUserDTO user, CancellationToken cancellationToken) {
            var subject = _configuration.GetValue<string>("UserData:RegisterSubject");
            var message = _configuration.GetValue<string>("UserData:RegisterMessage");
            var repoResult = await _userRepository.Register(user, cancellationToken);
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewUserDTO>.Failure(repoResult.Message);
            }  
            return OperationResult<ViewUserDTO>.Success(repoResult.Entity, "Succesfully registered");
        }

        public async Task<OperationResult<ViewUserDTO>> UpdateUser(int id,UpdateUserDTO user, CancellationToken cancellationToken) {
            var repoResult = await _userRepository.UpdateUser(id,user, cancellationToken);
            if (!repoResult.IsSuccesful) {
                return OperationResult<ViewUserDTO>.Failure(repoResult.Message);
            }
            return OperationResult<ViewUserDTO>.Success("Successfully updated user");
        }
    }
}
