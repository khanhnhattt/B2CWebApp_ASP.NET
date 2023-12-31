﻿using System;
using System.Collections.Generic;

namespace B2CWebApp.Models;

public partial class UserRole
{
    public long RoleId { get; set; }

    public long UserId { get; set; }

    public virtual Role Role { get; set; }

    public virtual User User { get; set; }
}
