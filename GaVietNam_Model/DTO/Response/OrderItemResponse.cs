using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Response
{
    public class OrderItemResponse
    {
        public long Id { get; set; }

        public long KindId { get; set; }

        public long OrderId { get; set; }

        public int OrderQuantity { get; set; }

        public double OrderPrice { get; set; }

        public string ChickenName { get; set; }

        public string KindName { get; set; }
    }
}
