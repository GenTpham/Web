using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string? RoomType { get; set; }
        public int RoomNumber { get; set; }

        public string? Status { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? category { get; set; }
    }
}
