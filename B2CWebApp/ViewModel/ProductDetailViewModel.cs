using B2CWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2CWebApp.ViewModel
{
    public class ProductDetailViewModel
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string ProductType { get; set; }
        public List<string> ProductImages { get; set; }
        public List<string> Capacities { get; set; }
        [NotMapped]
        public Dictionary<string, int?> Stocks { get; set; }
    }

}
