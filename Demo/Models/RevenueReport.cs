using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Demo.Models
{
	public class RevenueReport
	{
		[Key]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public decimal TotalRevenue { get; set; }
		public decimal TotalExpense { get; set; }
		public decimal NetProfit { get; set; }
	}
}
