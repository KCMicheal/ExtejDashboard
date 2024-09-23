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
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        // Get all products
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Add product to cart
        public async Task AddToCartAsync(Guid productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                var cartItem = new CartItem
                {
                    Product = product,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        // Retrieve all cart items
        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems.Include(ci => ci.Product).ToListAsync();
        }

        // Checkout and save the order
        public async Task CheckoutAsync(List<CartItem> cartItems)
        {
            var order = new Order
            {
                Items = cartItems,
                OrderDate = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(ci => ci.TotalPrice)
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);  // Clear cart after checkout
            await _context.SaveChangesAsync();
        }
    }

}
