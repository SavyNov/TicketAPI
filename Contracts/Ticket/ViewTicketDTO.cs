using TicketAPI.Contracts.User;

namespace TicketAPI.Contracts.Ticket {
    public class ViewTicketDTO {
        public required string Type { get; set; }
        public required DateTime DateBought { get; set; }
        public required DateTime TravelDate { get; set; }
        public required double Price { get; set; }
        public string TicketName { get; set; }
        public required int Adult { get; set; }
        public required int Toddler { get; set; }
        public required int Baby { get; set; }
    }
}
