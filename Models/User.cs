using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models {
    public class User {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public ICollection<Ticket> Tickets { get; } = new List<Ticket> { };
    }
}
