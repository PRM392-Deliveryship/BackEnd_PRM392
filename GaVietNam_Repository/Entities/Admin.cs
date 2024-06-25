using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entities;

public partial class Admin
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? User { get; set; }
}
