using B2CWebApp.Models.ViewModel;
using B2CWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2CWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public IActionResult ViewByCategory(string cate)
        {
            List<ProductsViewModel> products = _productService.findByCategory(cate);
            ViewBag.category = _productService.findNameById(cate);
            ViewBag.products = products;
            return View();
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            List<ProductsViewModel> products = _productService.search(search);
            if (products == null || products.Count == 0) 
            {
                ViewBag.msg = "No item found";
                return View();
            }
            ViewBag.products = products; 
            ViewBag.search = search;
            return View();
        }

        [HttpGet]
        public IActionResult Details(string id, string? cf)
        {
            if (cf !=null ) { ViewBag.cf = cf; }
            long productId = long.Parse(id);
            ProductDetailViewModel product = _productService.findById(productId);
            ViewBag.product = product;
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(IFormCollection collection)
        {
            long productId = long.Parse(collection["id"]);
            int quantity = int.Parse(collection["quantity"]);

            // Check for logged in
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            _productService.AddToCart(productId, quantity, userId);
            return RedirectToAction("Details", "Product", new { id = productId, cf = "Added to Cart" });
        }

        public IActionResult DecreaseOneInCart(string id) 
        {
            long productId = long.Parse(id);
            int quantity = -1;

            // Check for logged in
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            _productService.AddToCart(productId, quantity, userId);
            return RedirectToAction("ViewCart", "Cart");
        }

        public IActionResult IncreaseOneInCart(string id)
        {
            long productId = long.Parse(id);
            int quantity = 1;

            // Check for logged in
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            _productService.AddToCart(productId, quantity, userId);
            return RedirectToAction("ViewCart", "Cart");
        }
    }
}
