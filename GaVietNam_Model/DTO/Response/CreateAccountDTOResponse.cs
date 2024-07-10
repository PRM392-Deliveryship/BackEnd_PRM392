using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Response
{
    public class CreateAccountDTOResponse
    {
        public long RoleId { get; set; }
        public required string Username { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
    }
}
