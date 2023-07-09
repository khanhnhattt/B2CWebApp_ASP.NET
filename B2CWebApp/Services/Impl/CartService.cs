using B2CWebApp.Models;
using B2CWebApp.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace B2CWebApp.Services.Impl
{
    public class CartService : ICartService
    {
        B2cContext _context = new B2cContext();
        public List<CartViewModel> ViewCart(string userId)
        {
            long uId = long.Parse(userId);
            User user = _context.Users.Find(uId);

            if (user == null)
            {
                return null;
            }

            List<Cart> carts = _context.Carts
                .Where(c => c.OrderId == null && c.UserId == uId)
                .ToList();

            List<CartViewModel> cartViewModels = carts.Select(c => new CartViewModel
            {
                CartId = c.Id,
                Price = c.Price,
                ProductName = c.Product.Name,
                Quantity = c.Quantity
            }).ToList();

            return cartViewModels;
        }
    }
}
