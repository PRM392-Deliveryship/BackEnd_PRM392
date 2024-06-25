using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entities;

public partial class Kind
{
    public long Id { get; set; }

    public string KindName { get; set; } = null!;

    public string? Image { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Chicken> Chickens { get; set; } = new List<Chicken>();
}
