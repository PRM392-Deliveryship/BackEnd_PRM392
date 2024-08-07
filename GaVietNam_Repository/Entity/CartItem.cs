﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Repository.Entity
{
    public class CartItem
    {
        public long Id { get; set; }

        public long CartId { get; set; }

        public long KindId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("KindId")]
        public virtual Kind Kind { get; set; }
    }
}
