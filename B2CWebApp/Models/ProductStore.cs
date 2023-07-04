using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class ProductStore
{
    public long StoreId { get; set; }

    public long ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; }

    public virtual Store Store { get; set; }
}
