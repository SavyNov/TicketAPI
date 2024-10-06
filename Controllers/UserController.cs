using Microsoft.AspNetCore.Mvc;
using TicketAPI.Contracts.Ticket;
using TicketAPI.Contracts.User;
using TicketAPI.Services.UserServices;

namespace TicketAPI.Controllers {
    [Route("api/users")]
    [ApiController]
    public class UserController: ControllerBase {

        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService=userService;
        }

        [HttpGet]
        public async Task<List<ViewUserDTO>> GetAllUsers(CancellationToken cancellationToken) {
            return await _userService.GetAllUsers(cancellationToken);          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewUserDTO>> GetUser(int id, CancellationToken cancellationToken) {
            var result = await _userService.GetUser(id, cancellationToken);
            if (result is null)
                return NotFound("User not found");
            return Ok(result);
        }

        [HttpGet("{id}/Ticket")]
        public async Task<ActionResult<List<ViewTicketDTO>>> GetUserTickets(int id, CancellationToken cancellationToken) {
            var result = _userService.CheckTickets(id, cancellationToken);
            if (result is null)
                return BadRequest("User has no tickets");
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<ViewUserDTO>> DeleteUser(int id, CancellationToken cancellationToken) {
            var result = await _userService.DeleteUser(id, cancellationToken);
            if (result is null)
                return BadRequest("User not found");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ViewUserDTO>> UpdateUser(int id, UpdateUserDTO request, CancellationToken cancellationToken) {
            var result = await _userService.UpdateUser(id, request, cancellationToken);
            if (result is null)
                return NotFound("User not found");
            return Ok(result);
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ViewUserDTO>> Register(InputUserDTO request, CancellationToken cancellationToken) {
            var result = await _userService.Register(request, cancellationToken);
            if (result is null)
                return BadRequest("User already exists");
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ViewUserDTO>> Login(LoginUserDTO request, CancellationToken cancellationToken) {
            var result = await _userService.Login(request, cancellationToken);
            if (result is null)
                return BadRequest("Invalid login details");
            return Ok(result);
        }
    }
}