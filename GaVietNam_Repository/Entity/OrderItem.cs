using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaVietNam_Repository.Entity;

public class OrderItem
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long KindId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    [ForeignKey("KindId")]
    public virtual Kind Kind { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
}
