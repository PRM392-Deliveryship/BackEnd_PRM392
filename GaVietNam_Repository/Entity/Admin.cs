using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaVietNam_Repository.Entity;

public partial class Admin
{
    public long Id { get; set; }

    public long RoleId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool Status { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
}
