using GaVietNam_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Response
{
    public class CartResponse
    {
        public List<CartItem> Items { get; set; }
        public double TotalPrice { get; set; }
    }
}
