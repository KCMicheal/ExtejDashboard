﻿using ExtejDashboard_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtejDashboard_Domain.ExtejDashboardContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

}
