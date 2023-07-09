using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class Order
{
    public long Id { get; set; }

    public DateTime? OrderTime { get; set; }

    public long? ShipperId { get; set; }

    public long User { get; set; }

    public string Address { get; set; }

    public string OrderStatus { get; set; }

    public string ShippingStatus { get; set; }

    public string Tel { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual OrderInvoice OrderInvoice { get; set; }

    public virtual Shipper Shipper { get; set; }

    public virtual User UserNavigation { get; set; }
}
