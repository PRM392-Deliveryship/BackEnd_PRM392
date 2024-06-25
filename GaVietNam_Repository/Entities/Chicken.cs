using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entities;

public partial class Chicken
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? KindId { get; set; }

    public decimal Price { get; set; }

    public string WholeOrHalf { get; set; } = null!;

    public int Stock { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public virtual Kind? Kind { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
