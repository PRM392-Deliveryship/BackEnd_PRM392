using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaVietNam_Repository.Entity;

public class Bill
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public string Status { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
}
