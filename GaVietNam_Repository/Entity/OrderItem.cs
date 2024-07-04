using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaVietNam_Repository.Entity;

public partial class OrderItem
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long ChickenId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    [ForeignKey("ChickenId")]
    public virtual Chicken Chicken { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
}
