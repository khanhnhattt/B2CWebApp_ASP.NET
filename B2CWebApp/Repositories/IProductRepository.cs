using B2CWebApp.ViewModel;

namespace B2CWebApp.Repositories
{
    public interface IProductRepository
    {
        List<ProductsViewModel> FindByCategoryId(long catId);
    }
}
