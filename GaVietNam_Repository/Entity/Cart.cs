using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Repository.Entity
{
    public class Cart
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public double TotalPrice { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
