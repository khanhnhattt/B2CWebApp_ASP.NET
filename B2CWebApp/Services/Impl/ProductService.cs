using B2CWebApp.Models;
using B2CWebApp.Models.ViewModel;
using B2CWebApp.Repositories;
using Microsoft.VisualBasic.CompilerServices;

namespace B2CWebApp.Services.Impl
{
    public class ProductService : IProductService
    {
        B2cContext _context = new B2cContext();
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

        public ProductDetailViewModel findById(long id)
        {
            ProductDetailViewModel product = _productRepository.FindById(id);
            if (product == null)
            {
                return null;
            }
            return product;
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

        public void AddToCart(long productId, int quantity, string userId)
        {
            long uId = long.Parse(userId);
            User user = _context.Users.Find(uId);

            if (user == null)
            {
                return;
            }

            Product product = _productRepository.FindProductById(productId);

            long price = product.Price*quantity;

            //Cart cart = _cartRepository.FindByUserAndProduct(user.Id, product.Id);
            Cart cart = (Cart)_context.Carts.Where(c => c.UserId == user.Id && c.ProductId == productId).FirstOrDefault();

            if (cart == null)
            {
                Cart c = new Cart()
                {
                    Price = price,
                    Quantity = quantity,
                    ProductId = productId,
                    UserId = user.Id
                };
                _context.Carts.Add(c);
                _context.SaveChanges();
            }
            else
            {
                price += cart.Price;
                cart.Price = price;

                int oldQuantity = cart.Quantity;
                cart.Quantity = quantity+oldQuantity;

                _context.Carts.Update(cart);
                _context.SaveChanges();
            }

        }
    }
}
