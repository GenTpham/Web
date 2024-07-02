﻿using System;
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
    }
}
