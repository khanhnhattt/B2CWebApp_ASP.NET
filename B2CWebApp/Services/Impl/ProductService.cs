using B2CWebApp.Repositories;
using B2CWebApp.ViewModel;
using Microsoft.VisualBasic.CompilerServices;

namespace B2CWebApp.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public List<ProductsViewModel> findByCategory(string cate)
        {
            long catId = long.Parse(cate);
            List<ProductsViewModel> productsViewModels = _productRepository.FindByCategoryId(catId);
            return productsViewModels;
        }

        public string findNameById(string cate)
        {
            long catId = long.Parse(cate);
            string category = _productRepository.FindCategoryByCatId(catId).Name;
            return category;
        }

        public List<ProductsViewModel> search(string search)
        {
            List<ProductsViewModel> productsViewModels = _productRepository.Search(search);
            if (productsViewModels == null)
            {
                return null;
            }
            return productsViewModels;
        }
    }
}
