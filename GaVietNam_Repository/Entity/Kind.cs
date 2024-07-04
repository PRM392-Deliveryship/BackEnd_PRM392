using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaVietNam_Repository.Entity;

public partial class Kind
{
    public long Id { get; set; }

    public long ChickenId { get; set; }
    
    public string KindName { get; set; }

    public string Image { get; set; }

    public int Quantity { get; set; }

    public bool Status { get; set; }

    [ForeignKey("ChickenId")]
    public virtual Chicken Chicken { get; set; }
}
