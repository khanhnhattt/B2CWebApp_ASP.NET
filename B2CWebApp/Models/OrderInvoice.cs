using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class OrderInvoice
{
    public DateTime? Date { get; set; }

    public long Id { get; set; }

    public long? OrderId { get; set; }

    public string InvoiceNumber { get; set; }

    public string PaymentMethod { get; set; }

    public string PaymentStatus { get; set; }

    public virtual Order Order { get; set; }
}
