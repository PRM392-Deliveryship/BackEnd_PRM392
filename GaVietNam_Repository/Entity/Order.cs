﻿using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entity;

public partial class Order
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? AdminId { get; set; }

    public string? OrderRequirement { get; set; }

    public string? OrderCode { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? CreateDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }
}
