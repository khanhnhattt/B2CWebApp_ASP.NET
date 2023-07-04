using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class Shipper
{
    public long Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
