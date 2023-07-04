using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class ProductImage
{
    public long Id { get; set; }

    public string ImgPath { get; set; }

    public long? ProductId { get; set; }

    public virtual Product Product { get; set; }
}
