using B2CWebApp.Models.Enums;
using B2CWebApp.Models.ViewModel;
using B2CWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2CWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }

        public IActionResult ViewCart()
        {
            // Check for logged in
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<CartViewModel> cartViewModels = _cartService.ViewCart(userId);
            ViewBag.carts = cartViewModels;
            return View();
        }

        public IActionResult CheckOut()
        {
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            CheckoutViewModel checkOutViewModel = _cartService.ViewCheckout(userId);
            ViewBag.checkout = checkOutViewModel;
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(IFormCollection collection)
        {
            string method = collection["method"];
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            PaymentMethod paymentMethod = (PaymentMethod) Enum.Parse(typeof(PaymentMethod), method, true);

            string message = _cartService.PlaceOrder(paymentMethod, userId);

            return RedirectToAction("ViewOrders", "Cart");
        }

        public IActionResult ViewOrders()
        {
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            List<OrdersViewModel> ordersViewModels = _cartService.ViewOrders(userId);
            ViewBag.orders = ordersViewModels;
            return View();
        }

        public IActionResult ViewOrderDetail(string id)
        {
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            OrderDetailViewModel orderDetailViewModel = _cartService.ViewOrderDetailById(id, userId);
            ViewBag.order = orderDetailViewModel;
            return View();
        }
    }
}
