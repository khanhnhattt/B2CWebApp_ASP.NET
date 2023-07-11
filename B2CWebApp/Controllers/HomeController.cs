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
            
            List<ProductType> productTypes = context.ProductTypes.Include(pt => pt.Products).ToList();

            foreach (ProductType productType in productTypes)
            {
                foreach

                list.Add(new HomeViewModel
                {
                    ProductTypeId = productType.Id, 
                    ProductName = productType.Products.,
                    ProductTypeName = productType.Name,

                });
            }

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