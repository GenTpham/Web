using Org.BouncyCastle.Crypto.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone {  get; set; }
        public DateTime? Date { get; set; }
        public string? Message { get; set; }
        public int Point {  get; set; }
      
    }
}
