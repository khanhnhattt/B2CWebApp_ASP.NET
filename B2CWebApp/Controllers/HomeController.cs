using B2CWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using B2CWebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace B2CWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            B2cContext context = new B2cContext();
            List<HomeViewModel> list = new List<HomeViewModel>();
            
            List<ProductType> productTypes = context.ProductTypes.Include(pt => pt.Products.Take(4)).ToList();

            foreach (ProductType productType in productTypes)
            {
                List<HomeProductViewModel> productsViewModels = new List<HomeProductViewModel>();
                foreach (var p in productType.Products)
                {   
                    productsViewModels.Add(new HomeProductViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImgPath = context.ProductImages.Where(img => img.ProductId == p.Id).FirstOrDefault().ImgPath,
                        Price = p.Price
                    });
                }

                list.Add(new HomeViewModel
                {
                    ProductTypeId = productType.Id,
                    ProductTypeName = productType.Name,
                    Products = productsViewModels
                });
            }
            ViewBag.productTypes = list;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}