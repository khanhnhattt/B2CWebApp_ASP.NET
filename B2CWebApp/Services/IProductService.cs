using B2CWebApp.ViewModel;

namespace B2CWebApp.Services
{
    public interface IProductService
    {
        List<ProductsViewModel> findByCategory(string cat);
        string findNameById(string cate);
        List<ProductsViewModel> search(string search);
    }
}
