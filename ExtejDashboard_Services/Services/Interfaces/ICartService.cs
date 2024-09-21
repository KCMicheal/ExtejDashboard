using ExtejDashboard_Domain.Models;

namespace ExtejDashboard_Services.Services.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(CartItem cartItem);
        Task CheckoutAsync(List<CartItem> cartItems);
        Task<List<CartItem>> GetCartItemsAsync();
    }
}
