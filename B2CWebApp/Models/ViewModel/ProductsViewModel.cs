using System.ComponentModel.DataAnnotations.Schema;

namespace B2CWebApp.Models.ViewModel
{
    public class ProductsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string ProductType { get; set; }

        [NotMapped]
        public Dictionary<string, int?> Stocks { get; set; }
    }
}
