using B2CWebApp.Models.Enums;
using B2CWebApp.Models.ViewModel;

namespace B2CWebApp.Services
{
    public interface ICartService
    {
        List<CartViewModel> ViewCart(string userId);
        CheckoutViewModel ViewCheckout(string userId);
        string PlaceOrder(PaymentMethod paymentMethod, string address, string userId, string tel);
        List<OrdersViewModel> ViewOrders(string userId);
        OrderDetailViewModel ViewOrderDetailById(string id, string userId);
    }
}