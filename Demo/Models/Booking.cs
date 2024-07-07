using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Demo.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set;}
        public string? Adult { get; set; }
        public string? Child { get; set; }
        public string? Request { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room? room { get; set; }

    }
}
