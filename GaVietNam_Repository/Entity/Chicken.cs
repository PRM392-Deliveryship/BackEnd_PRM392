using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entity;

public partial class Chicken
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Kind> Kinds { get; set; } = new List<Kind>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
