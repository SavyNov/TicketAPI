using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketAPI.Models {
    public class Ticket {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Type { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TicketNumber { get; set; }
        public required DateTime DateBought { get; set; }
        public required DateTime TravelDate { get; set; }
        public required double Price { get; set; }
        public string TicketName { get; set; }
        public required int Adult { get; set; }
        public required int Toddler { get; set; }
        public required int Baby { get; set; }
        public User? User { get; set; }

        public Ticket() {
            
        }
    }
}
