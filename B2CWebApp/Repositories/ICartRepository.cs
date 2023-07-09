using B2CWebApp.Models;

namespace B2CWebApp.Repositories
{
    public interface ICartRepository
    {
        void add(Cart cart);
        void Update(Cart cart);
        Cart FindByUserAndProduct(long id1, long id2);
    }
}
