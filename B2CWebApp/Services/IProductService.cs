using B2CWebApp.Models.ViewModel;

namespace B2CWebApp.Services
{
    public interface IProductService
    {
        List<ProductsViewModel> findByCategory(string cat);
        string findNameById(string cate);
        List<ProductsViewModel> search(string search);
        ProductDetailViewModel findById(long id);
        void AddToCart(long productId, int quantity, string UserId);
    }
}
