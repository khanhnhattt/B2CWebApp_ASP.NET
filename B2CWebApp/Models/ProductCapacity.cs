using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class ProductCapacity
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Capacity { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
