using B2CWebApp.Models;
using B2CWebApp.Models.ViewModel;
using System.Collections.Generic;

namespace B2CWebApp.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly B2cContext _context = new B2cContext();

        public ProductRepository()
        {
        }

        public List<ProductsViewModel> FindByCategoryId(long catId)
        {
            List<Product> products = _context.Products.Where(p => p.ProductTypeId == catId).ToList();
            List<ProductsViewModel> productsViewModel = new List<ProductsViewModel>();
            foreach (var p in products)
            {
                List<ProductStore> productStores = _context.ProductStores.Where(ps => ps.ProductId == p.Id).ToList();
                Dictionary<string, int?> stocks = new Dictionary<string, int?>();
                foreach (var ps in productStores)
                {
                    string storeName = _context.Stores.Find(ps.StoreId).Name;
                    int? quantity = ps.Quantity;
                    stocks.Add(storeName, quantity);
                }

                string productTypeName = _context.ProductTypes.Find(p.ProductTypeId).Name;

                productsViewModel.Add(new ProductsViewModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    ProductType = productTypeName,
                    Stocks = stocks
                });
            }
            return productsViewModel;
        }

        public ProductDetailViewModel FindById(long id)
        {
            Product product = _context.Products.Find(id);
            ProductDetailViewModel productDvm = new ProductDetailViewModel();
            if (product == null)
            {
                return null;
            }

            List<ProductStore> productStores = _context.ProductStores.Where(ps => ps.ProductId == product.Id).ToList();
            Dictionary<string, int?> stocks = new Dictionary<string, int?>();
            foreach (var ps in productStores)
            {
                string storeName = _context.Stores.Find(ps.StoreId).Name;
                int? quantity = ps.Quantity;
                stocks.Add(storeName, quantity);
            }

            string productTypeName = _context.ProductTypes.Find(product.ProductTypeId).Name;

            List<ProductImage> productImages = _context.ProductImages.Where(pi => pi.ProductId == product.Id).ToList();
            List<string> imgPaths = new List<string>();
            foreach (var imgPath in productImages)
            {
                imgPaths.Add(imgPath.ImgPath);
            }

            if (productTypeName.Equals("Macbook"))
            {
                productDvm.Capacities = new List<string> { "256GB", "512GB", "1TB", "2TB" };
            }
            else if (productTypeName.Equals("Accessories") || productTypeName.Equals("Watch"))
            {
                productDvm.Capacities = null;
            }
            else
            {
                productDvm.Capacities = new List<string> { "128GB", "256GB", "512GB" };
            }

            productDvm.Id = product.Id;
            productDvm.Description = product.Description;
            productDvm.Name = product.Name;
            productDvm.Price = product.Price;
            productDvm.Stocks = stocks;
            productDvm.ProductType = productTypeName;
            productDvm.ProductImages = imgPaths;

            return productDvm;
        }

        public ProductType FindCategoryByCatId(long catId)
        {
            ProductType category = _context.ProductTypes.Find(catId);
            return category;
        }

        public Product FindProductById(long id)
        {
            return _context.Products.Find(id);
        }

        public List<ProductsViewModel> Search(string search)
        {
            List<Product> products = _context.Products.Where(p => p.Name.Contains(search)).ToList();
            if (products == null)
            {
                return null;
            }
            List<ProductsViewModel> productsViewModel = new List<ProductsViewModel>();
            foreach (var p in products)
            {
                List<ProductStore> productStores = _context.ProductStores.Where(ps => ps.ProductId == p.Id).ToList();
                Dictionary<string, int?> stocks = new Dictionary<string, int?>();
                foreach (var ps in productStores)
                {
                    string storeName = _context.Stores.Find(ps.StoreId).Name;
                    int? quantity = ps.Quantity;
                    stocks.Add(storeName, quantity);
                }

                string productTypeName = _context.ProductTypes.Find(p.ProductTypeId).Name;

                productsViewModel.Add(new ProductsViewModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    ProductType = productTypeName,
                    Stocks = stocks
                });
            }
            return productsViewModel;
        }
    }
}
