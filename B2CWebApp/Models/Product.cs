using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class Product
{
    public long Id { get; set; }

    public ulong Available { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }

    public long Price { get; set; }

    public long ProductTypeId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual ProductType ProductType { get; set; }
}
