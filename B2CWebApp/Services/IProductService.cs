using B2CWebApp.ViewModel;

namespace B2CWebApp.Services
{
    public interface IProductService
    {
        List<ProductsViewModel> findByCategory(string cat);
    }
}
