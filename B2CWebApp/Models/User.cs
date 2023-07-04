using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class User
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public string Username { get; set; }

    public string Address { get; set; }

    public string Tel { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ResetPasswordToken ResetPasswordToken { get; set; }
}
