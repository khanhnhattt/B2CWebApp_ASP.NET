namespace B2CWebApp.Models.ViewModel
{
    public class HomeViewModel
    {
        public long ProductTypeId { get; set; }
        public string ProductTypeName { get; set; } 
        public List<HomeProductViewModel> Products { get; set; }
    }
}
