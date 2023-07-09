using B2CWebApp.ViewModel;

namespace B2CWebApp.Services
{
    internal interface ICartService
    {
        List<CartViewModel> ViewCart(string userId);
    }
}