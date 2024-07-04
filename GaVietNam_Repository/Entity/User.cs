using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaVietNam_Repository.Entity;

public partial class User
{
    public long Id { get; set; }

    public long RoleId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }

    public string Avatar { get; set; }

    public string Gender { get; set; }

    public string IdentityCard { get; set; }

    public DateTime Dob { get; set; }

    public string Phone { get; set; }

    public DateTime CreateDate { get; set; }

    public bool Status { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
}
