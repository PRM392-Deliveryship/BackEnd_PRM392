using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entity;

public partial class Bill
{
    public long Id { get; set; }

    public long? OrderId { get; set; }

    public string? Status { get; set; }

    public virtual Order? Order { get; set; }
}
