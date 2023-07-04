﻿using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class ProductType
{
    public long Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
