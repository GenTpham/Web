using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class FinancialReport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReportDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total Revenue must be a positive number")]
        public decimal TotalRevenue { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total Expenses must be a positive number")]
        public decimal TotalExpenses { get; set; }

        public decimal NetProfit => TotalRevenue - TotalExpenses;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number of Bookings must be a positive integer")]
        public int NumberOfBookings { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }
    }
}