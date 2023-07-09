using B2CWebApp.Models;

namespace B2CWebApp.Repositories.Impl
{
    public class CartRepository : ICartRepository
    {
        private readonly B2cContext _context = new B2cContext();

        public void add(Cart cart)
        {
            _context.Add(cart);
            _context.SaveChanges();
        }
        public void Update(Cart cart)
        {
            _context.Update(cart);
            _context.SaveChanges();
        }

        public Cart FindByUserAndProduct(long userId, long productId)
        {
            Cart cart = (Cart)_context.Carts.Where(c => c.UserId == userId && c.ProductId == productId);
            return cart;
        }
    }
}
