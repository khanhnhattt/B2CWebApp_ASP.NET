using B2CWebApp.Models;
using B2CWebApp.Models.Enums;
using B2CWebApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2CWebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly B2cContext _context = new B2cContext();
        public IActionResult Index()
        {
            List<Order> list = _context.Orders.Include(o => o.OrderInvoice).Include(o => o.Shipper).Include(o => o.UserNavigation).ToList();
            List<AdminOrdersViewModel> orders = new List<AdminOrdersViewModel>();
            foreach (Order order in list)
            {
                orders.Add(new AdminOrdersViewModel()
                {
                    Id = order.Id,
                    InvoiceNumber = order.OrderInvoice.InvoiceNumber,
                    ShippingStatus = order.ShippingStatus,
                    OrderStatus = order.OrderStatus,
                    Address = order.Address,
                    Tel = order.Tel,
                    OrderTime = order.OrderTime,
                    PaymentStatus = order.OrderInvoice.PaymentStatus,
                    CustomerId = order.UserNavigation.Id.ToString(),
                    CustomerName = order.UserNavigation.Name,
                });
            }
            ViewBag.Orders = orders;
            return View(orders);
        }

        [HttpPost]
        public IActionResult ChangeOrderStatus(string orderId, string orderStatus)
        {
            string message = null;

            // Check for logged in
            string userId = HttpContext.Session.GetString("u");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            long uId = long.Parse(userId);
            User user = _context.Users.Find(uId);
            UserRole role = _context.UserRoles.FirstOrDefault(r => r.UserId == uId);
            if (role.RoleId != 1)
            {
                message = "Access Denied";
            }


            long oId = long.Parse(orderId);
            OrderStatus status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus, true);

            Order order = _context.Orders.Find(oId);
            if (order != null)
            {
                order.OrderStatus = status.ToString();
                _context.Orders.Update(order);
                _context.SaveChanges();
                message = $"Order {oId}'s status changed successfully";
            }

            return RedirectToAction("Index", "Admin", new { msg = message });

        }
    }
}
