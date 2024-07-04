using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Request
{
    public class KindRequest
    {
        [Required(ErrorMessage = "ProudctId is required")]
        public long ChickenId { get; set; }

        [Required(ErrorMessage = "Kind name is required")]
        public string KindName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must greater than or equal to 0")]
        public int Quantity { get; set; }

        public IFormFile? File { get; set; }
    }
}
