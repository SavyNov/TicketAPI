using Microsoft.AspNetCore.Mvc;
using TicketAPI.Contracts.Ticket;
using TicketAPI.Services.TicketServices;

namespace TicketAPI.Controllers {
    [Route("api/tickets")]
    [ApiController]
    public class TicketController: ControllerBase {

        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService) {
            _ticketService=ticketService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ViewTicketDTO>>> GetAllTickets(CancellationToken cancellationToken) {
            return await _ticketService.GetAllTickets(cancellationToken);  
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ViewTicketDTO>> GetTicket(int id, CancellationToken cancellationToken) {
            var result = await _ticketService.GetTicket(id, cancellationToken);
            if (!result.IsSuccesful)
                return NotFound("Ticket not found");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ViewTicketDTO>> DeleteTicket(int id, CancellationToken cancellationToken) {
            var result = await _ticketService.DeleteTicket(id, cancellationToken);
            if (!result.IsSuccesful)
                return NotFound("Ticket not found");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ViewTicketDTO>> UpdateTicket(int id, InputTicketDTO requestDTO, CancellationToken cancellationToken) {
            var result = await _ticketService.UpdateTicket(id,requestDTO, cancellationToken);
            if (!result.IsSuccesful)
                return NotFound("Ticket not found");   
            return Ok(result);
        }

        [HttpPost("/buy")]
        public async Task<ActionResult<ViewTicketDTO>> Buy(InputTicketDTO request, int userId, CancellationToken cancellationToken) {
            var result = await _ticketService.Buy(request, userId, cancellationToken);
            if (!result.IsSuccesful)
                return NotFound("Ticket not found");    
            return Ok(result);
        }
    }
}