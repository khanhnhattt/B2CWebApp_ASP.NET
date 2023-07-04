using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class ProductOrder
{
    public long Id { get; set; }

    public long Price { get; set; }

    public int Quantity { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public virtual Feedback Feedback { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}
