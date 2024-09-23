using ExtejDashboard_Domain.Models;

namespace ExtejDashboard_Services.Services.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(Guid productId, int quantity);
        Task CheckoutAsync(List<CartItem> cartItems);
        Task<List<CartItem>> GetCartItemsAsync();
    }
}
