using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Response
{
    public class CartItemResponse
    {
        public long Id { get; set; }
        
        public long CartId { get; set; }
        
        public long KindId { get; set; }
        
        public string Image { get; set; }

        public int Quantity { get; set; }
        
        public double Price { get; set; }
    }
}
