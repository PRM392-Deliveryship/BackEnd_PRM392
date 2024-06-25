using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entities;

public partial class OrderItem
{
    public long Id { get; set; }

    public long? OrderId { get; set; }

    public long? ChickenId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Chicken? Chicken { get; set; }

    public virtual Order? Order { get; set; }
}
