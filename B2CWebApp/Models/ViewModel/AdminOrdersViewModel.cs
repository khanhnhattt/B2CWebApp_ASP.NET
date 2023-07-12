namespace B2CWebApp.Models.ViewModel
{
    public class AdminOrdersViewModel
    {
        public long Id { get; set; }
        public DateTime? OrderTime { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string ShippingStatus { get; set; }
        public string OrderStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerId { get; set; } 
        public string CustomerName { get; set; }
        public string PaymentStatus { get; set; }
    }
}
