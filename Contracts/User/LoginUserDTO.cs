﻿namespace TicketAPI.Contracts.User {
    public class LoginUserDTO {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
