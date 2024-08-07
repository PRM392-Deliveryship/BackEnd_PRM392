﻿using System;
using System.Collections.Generic;

namespace GaVietNam_Repository.Entity;

public class Chicken
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public bool Status { get; set; }

}
