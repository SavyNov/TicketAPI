using TicketAPI.Contracts.Ticket;

namespace TicketAPI.Contracts.User {
    public class ViewUserDTO {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public List<ViewTicketDTO> Tickets { get; set; }
    }
}
