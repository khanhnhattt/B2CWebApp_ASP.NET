using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class Feedback
{
    public long Id { get; set; }

    public string Content { get; set; }

    public long Rating { get; set; }

    public long? ProductOrderId { get; set; }

    public virtual ProductOrder ProductOrder { get; set; }
}
