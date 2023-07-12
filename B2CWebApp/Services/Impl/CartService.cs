using B2CWebApp.Models;
using B2CWebApp.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using B2CWebApp.Models.Enums;
using B2CWebApp.Repositories.Impl;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;

namespace B2CWebApp.Services.Impl
{
    public class CartService : ICartService
    {
        B2cContext _context = new B2cContext();
        public List<CartViewModel> ViewCart(string userId)
        {
            long uId = long.Parse(userId);
            User user = _context.Users.Find(uId);

            if (user == null)
            {
                return null;
            }

            List<Cart> carts = _context.Carts
                .Where(c => c.OrderId == null && c.UserId == uId)
                .ToList();

            List<CartViewModel> cartViewModels = new List<CartViewModel>();

            foreach (var c in carts)
            {
                Product product = _context.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == c.ProductId);
                CartViewModel cartViewModel = new CartViewModel()
                {
                    CartId = c.Id,
                    Price = c.Price,
                    ProductName = product.Name,
                    Quantity = c.Quantity,
                    ProductType = _context.ProductTypes.Find(product.ProductTypeId).Name,
                    ProductImage = product.ProductImages.FirstOrDefault(c => c == c).ImgPath,
                    ProductId = product.Id
                };
                cartViewModels.Add(cartViewModel);
            }

            return cartViewModels;
        }

        public CheckoutViewModel ViewCheckout(string userId)
        {
            long uId = long.Parse(userId);
            User user = _context.Users.Find(uId);

            if (user == null)
            {
                return null;
            }

            List<CartViewModel> carts = ViewCart(userId);

            long totalPrice = 0;
            foreach (var cart in carts)
            {
                totalPrice += cart.Price;
            }

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel()
            {
                CartViewModels = carts,
                TotalPrice = totalPrice,
                Address = user.Address,
                Tel = user.Tel
            };

            return checkoutViewModel;
        }

        public string PlaceOrder(PaymentMethod paymentMethod, string address, string userId, string tel)
        {
            // Get user
            long uId = long.Parse(userId);
            User user = _context.Users.Find(uId);

            if (user == null)
            {
                return null;
            }

            List<CartViewModel> carts = ViewCart(userId);

            List<Cart> cartList = this.getStorage(uId);

            Optional<Shipper> shipper = (
                from s in _context.Shippers
                join o in _context.Orders on s.Id equals o.ShipperId into orderGroup
                orderby orderGroup.Count()
                select new Shipper()
                {
                    Id = s.Id,
                    Name = s.Name
                }
            ).FirstOrDefault();

            // Create new Order
            Order order = new Order()
            {
                OrderTime = DateTime.Now,
                Address = address,
                Tel = tel,
                //UserNavigation = user,
                User = uId,
                Carts = cartList,
                //Shipper = shipper.Value,
                ShipperId = shipper.Value.Id,
                OrderStatus = "UNCONFIRMED",
                ShippingStatus = "PROCESSING",
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Update OrderId in Cart
            long orderId = order.Id;
            _context.Carts
                .Where(c => c.UserId == uId && c.OrderId == null)
                .ToList()
                .ForEach(c => c.OrderId = orderId);
            _context.SaveChanges();

            string invoiceNumber = paymentMethod.ToString() + orderId + uId;

            PaymentStatus paymentStatus;
            if (paymentMethod.Equals(PaymentMethod.COD))
            {
                paymentStatus = PaymentStatus.UNPAID;
            }
            else
            {
                paymentStatus = PaymentStatus.PAID;
            }

            OrderInvoice invoice = new OrderInvoice()
            {
                InvoiceNumber = invoiceNumber,
                Date = order.OrderTime,
                PaymentMethod = paymentMethod.ToString(),
                PaymentStatus = paymentStatus.ToString(),
                Order = order,
                OrderId = orderId
            };

            _context.OrderInvoices.Add(invoice);
            _context.SaveChanges();

            return "Ordered Succesfully!";
        }

        public List<OrdersViewModel> ViewOrders(string userId)
        {
            // Get user
            long uId = long.Parse(userId);
            User user = _context.Users.Find(uId);

            if (user == null)
            {
                return null;
            }

            List<Order> orders = _context.Orders.Where(o => o.User == uId).Include(o => o.OrderInvoice).ToList();

            if (orders.IsNullOrEmpty())
                return null;
            List<OrdersViewModel> ordersViewModel = orders
                .Select(o => new OrdersViewModel()
                {
                    Id = o.Id,
                    InvoiceNumber = o.OrderInvoice.InvoiceNumber,
                    OrderTime = o.OrderTime,
                    ShippingStatus = o.ShippingStatus,
                    OrderStatus = o.OrderStatus,
                }).ToList();

            return ordersViewModel;
        }

        public OrderDetailViewModel ViewOrderDetailById(string id, string userId)
        {
            // Get user
            long uId = long.Parse(userId);
            User user = _context.Users.Find(uId);

            if (user == null)
            {
                return null;
            }

            long orderId = long.Parse(id);

            Optional<Order> order = _context.Orders.Include(o => o.OrderInvoice).Include(o => o.Carts).FirstOrDefault(o => o.Id == orderId);

            if (!order.HasValue)
            {
                return null;
            }

            if (order.Value.User == uId)
            {
                Order o = order.Value;
                long totalPrice = 0;
                List<CartViewModel> carts = new List<CartViewModel>();
                foreach (var c in o.Carts)
                {
                    Product product = _context.Products.Include(p => p.ProductCapacity).Include(p => p.ProductImages).FirstOrDefault(p => p.Id == c.ProductId);
                    CartViewModel cartViewModel = new CartViewModel()
                    {
                        CartId = c.Id,
                        Price = c.Price,
                        ProductName = product.Name,
                        Quantity = c.Quantity,
                        ProductType = _context.ProductTypes.Find(product.ProductTypeId).Name,
                        Capacity = product.ProductCapacity.Name,
                        ProductImage = product.ProductImages.FirstOrDefault(c => c == c).ImgPath
                    };
                    carts.Add(cartViewModel);
                    totalPrice += c.Price;
                }

                OrderDetailViewModel orderDetailViewModel = new OrderDetailViewModel()
                {
                    Id = o.Id,
                    Address = o.Address,
                    Tel = o.Tel,
                    ShippingStatus = o.ShippingStatus,
                    OrderStatus = o.OrderStatus,
                    InvoiceNumber = o.OrderInvoice.InvoiceNumber,
                    OrderTime = o.OrderTime,
                    Carts = carts,
                    TotalPrice = totalPrice
                };
                return orderDetailViewModel;
            }
            else
            {
                return null;
            }

        }

        private List<Cart> getStorage(long uId)
        {
            List<Cart> cartList = _context.Carts
                .Where(c => c.UserId == uId && c.OrderId == null)
                .Include(c => c.Product)
                .ToList();
            int size = cartList.Count;
            List<Store> stores = _context.Stores.ToList();

            while (size > 0)
            {
                cartList = _context.Carts
                    .Where(c => c.UserId == uId && c.OrderId == null)
                    .Include(c => c.Product)
                    .ToList();

                Dictionary<Store, List<Cart>> storeItemsAvailable = getStoreItemAvailable(cartList, stores);

                size -= updateStoreWithLongestItems(storeItemsAvailable);
            }

            return _context.Carts
                .Where(c => c.UserId == uId && c.OrderId == null)
                .Include(c => c.Product)
                .ToList();
        }

        private Dictionary<Store, List<Cart>> getStoreItemAvailable(List<Cart> carts, List<Store> stores)
        {
            Dictionary<Store, List<Cart>> storeItemsAvailable = new Dictionary<Store, List<Cart>>();
            foreach (var s in stores)
            {
                List<Cart> cartList = new List<Cart>();
                foreach (var c in carts)
                {
                    if (c.Store == null)
                    {
                        Optional<Product> p = _context.Products.FirstOrDefault(p => p.Name == c.Product.Name);
                        ProductStore ps = _context.ProductStores.Include(p => p.Product).FirstOrDefault(ps => ps.Product == p.Value && ps.Store == s);
                        if (ps != null && c.Quantity <= ps.Quantity)
                        {
                            cartList.Add(c);
                        }
                    }
                }
                storeItemsAvailable.Add(s, cartList);
            }
            return storeItemsAvailable;
        }

        private int updateStoreWithLongestItems(Dictionary<Store, List<Cart>> storeItemsAvailable)
        {
            Store chosenStore = new Store();
            List<Cart> carts = new List<Cart>();
            int longestSize = 0;

            // Find store w/ the longest items list
            foreach (var entry in storeItemsAvailable)
            {
                Store key = entry.Key;
                List<Cart> value = entry.Value;
                int size = value.Count;

                if (size > longestSize)
                {
                    chosenStore = key;
                    carts = value;
                    longestSize = size;
                }
            }

            // Update store in table Cart
            foreach (var c in carts)
            {
                Optional<Cart> cart = _context.Carts.Find(c.Id);
                cart.Value.Store = chosenStore;
                _context.Update(cart.Value);
                _context.SaveChanges();
            }

            return longestSize;
        }
    }
}
