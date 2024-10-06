namespace TicketAPI.Contracts.Ticket {

    public class InputTicketDTO {
        public string? Type { get; set; }
        public DateTime DateBought { get; set; }
        public DateTime TravelDate { get; set; }
        public double Price { get; set; }
        public string? TicketName { get; set; }
        public int Adult { get; set; }
        public int Toddler { get; set; }
        public int Baby { get; set; }
    }
}
