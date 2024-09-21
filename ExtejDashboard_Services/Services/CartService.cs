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
    public class CartService: ICartService
    {
        public AppDbContext _context { get; set; }
        public CartService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddToCartAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems.Include(ci => ci.Product).ToListAsync();
        }

        public async Task CheckoutAsync(List<CartItem> cartItems)
        {
            var order = new Order
            {
                Items = cartItems,
                OrderDate = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(ci => ci.TotalPrice)
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
