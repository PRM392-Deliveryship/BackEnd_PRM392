using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Repository.Entity
{
    public class CartItem
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public double ChickenPrice { get; set; }
        public string ChickenName { get; set; }
        public String KindImage { get; set; }
        public string KindName { get; set; }
    }
}
