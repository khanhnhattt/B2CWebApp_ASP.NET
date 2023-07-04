using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class Cart
{
    public long Id { get; set; }

    public long Price { get; set; }

    public int Quantity { get; set; }

    public long? OrderId { get; set; }

    public long ProductId { get; set; }

    public long StoreId { get; set; }

    public long UserId { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }

    public virtual Store Store { get; set; }

    public virtual User User { get; set; }
}
