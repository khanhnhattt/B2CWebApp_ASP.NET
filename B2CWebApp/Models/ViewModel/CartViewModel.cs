namespace B2CWebApp.Models.ViewModel
{
    public class CartViewModel
    {
        public long CartId { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductType { get; set; }
        public long ProductId { get; set; } 

        public string? Capacity { get; set; }

    }
}
