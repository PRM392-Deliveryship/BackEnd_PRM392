using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Request
{
    public class CartItemRequest
    {
        public long KindId { get; set; }
        public int Quantity { get; set; }
    }
}
