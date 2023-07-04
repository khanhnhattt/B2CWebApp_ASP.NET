using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class ResetPasswordToken
{
    public long Id { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string Token { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }
}
