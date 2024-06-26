using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entity;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string IdentityCard { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Phone { get; set; } = null!;

    public DateOnly? CreateDate { get; set; }

    public string Status { get; set; } = null!;

    public long? RoleId { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
