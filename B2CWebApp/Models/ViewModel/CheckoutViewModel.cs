namespace B2CWebApp.Models.ViewModel
{
    public class CheckoutViewModel
    {
        public List<CartViewModel> CartViewModels { get; set; }
        public long TotalPrice { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
    }
}
