using B2CWebApp.Services;
using B2CWebApp.ViewModel;
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
            ViewBag.products = products;
            return View();
        }
    }
}
