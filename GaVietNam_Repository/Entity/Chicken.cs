using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entity;

public partial class Chicken
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Avatar { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

}
