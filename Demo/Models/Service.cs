using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
	public class Service
	{
		[Key]
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public decimal? Price { get; set; }
		public string? Status { get; set; }
	}
}
