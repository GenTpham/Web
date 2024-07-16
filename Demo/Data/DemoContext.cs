using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Demo.Models;

namespace Demo.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext (DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        public DbSet<Demo.Models.Category> Category { get; set; } = default!;

        public DbSet<Demo.Models.Room>? Room { get; set; }

        public DbSet<Demo.Models.Employee>? Employee { get; set; }

        public DbSet<Demo.Models.Customer>? Customer { get; set; }

        public DbSet<Demo.Models.Booking>? Booking { get; set; }

        public DbSet<Demo.Models.User>? User { get; set; }

        public DbSet<Demo.Models.RevenueReport>? RevenueReport { get; set; }

        public DbSet<Demo.Models.Service>? Service { get; set; }

        public DbSet<Demo.Models.BookingService>? BookingService { get; set; }

        public DbSet<Demo.Models.FinancialReport>? FinancialReport { get; set; }

        public DbSet<Demo.Models.Promotion>? Promotion { get; set; }

        public DbSet<Demo.Models.Rating>? Rating { get; set; }
    }
}
