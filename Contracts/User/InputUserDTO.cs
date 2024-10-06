namespace TicketAPI.Contracts.User {
    public class InputUserDTO {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }
    }
}
