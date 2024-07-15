using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaVietNam_Repository.Entity;

public class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long AdminId { get; set; }

    public string OrderRequirement { get; set; }

    public string OrderCode { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime CreateDate { get; set; }

    public double TotalPrice { get; set; }

    public string Status { get; set; }

    [ForeignKey("AdminId")]
    public virtual Admin Admin { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
