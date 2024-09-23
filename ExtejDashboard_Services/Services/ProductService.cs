using ExtejDashboard_Domain.ExtejDashboardContext;
using ExtejDashboard_Domain.Models;
using ExtejDashboard_Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtejDashboard_Services.Services
{
    public class ProductService : IProductService
    {
        public AppDbContext _context { get; set; }
        public ProductService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
