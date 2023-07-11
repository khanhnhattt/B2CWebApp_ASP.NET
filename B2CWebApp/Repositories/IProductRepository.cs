using B2CWebApp.Models;
using B2CWebApp.Models.ViewModel;

namespace B2CWebApp.Repositories
{
    public interface IProductRepository
    {
        List<ProductsViewModel> FindByCategoryId(long catId);
        ProductType FindCategoryByCatId(long catId);
        List<ProductsViewModel> Search(string search);
        ProductDetailViewModel FindById(long id);
        Product FindProductById(long id);
    }
}
