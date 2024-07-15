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
        public long Id { get; set; }
        public long UserId { get; set; }
        public double TotalPrice { get; set; }
        public List<CartItemResponse> CartItems { get; set;}
    }

}
