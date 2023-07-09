using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class Store
{
    public long Id { get; set; }

    public string Location { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<ProductStore> ProductStores { get; set; } = new List<ProductStore>();
}
