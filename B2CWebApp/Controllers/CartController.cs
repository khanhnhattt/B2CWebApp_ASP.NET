using B2CWebApp.Services;
using B2CWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace B2CWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public IActionResult ViewCart()
        {
            // Check for logged in
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<CartViewModel> cartViewModels = _cartService.ViewCart(userId);
            return View();
        } 
            
    }
}
