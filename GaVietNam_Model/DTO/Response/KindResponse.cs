using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Response
{
    public class KindResponse
    {
        public long Id { get; set; }

        public long ChickenId { get; set; }

        public string KindName { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }

        public bool Status { get; set; }
    }
}
