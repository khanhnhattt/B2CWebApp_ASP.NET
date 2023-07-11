namespace B2CWebApp.Models.ViewModel
{
    public class OrderDetailViewModel
    {
        public long Id { get; set; }
        public DateTime? OrderTime { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string ShippingStatus { get; set; }
        public string OrderStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public List<CartViewModel> Carts { get; set; }
        public long TotalPrice { get; set; }
    }
}
