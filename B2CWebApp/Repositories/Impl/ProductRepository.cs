using B2CWebApp.Models;
using B2CWebApp.ViewModel;
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

        public ProductType FindCategoryByCatId(long catId)
        {
            ProductType category = _context.ProductTypes.Find(catId);
            return category;
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
