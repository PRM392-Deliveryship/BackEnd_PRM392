using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Request
{
    public class OrderRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string OrderRequirement { get; set; }

        public string PaymentMethod { get; set; }
    }
}
