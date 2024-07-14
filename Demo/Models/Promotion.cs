using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [Range(0, 100)]
        public int DiscountPercentage { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? PromoCode { get; set; }

        public bool IsActive { get; set; }
    }
}