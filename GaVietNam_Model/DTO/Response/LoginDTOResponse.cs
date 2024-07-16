using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Response
{
    public class LoginDTOResponse
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string RoleName { get; set; }
    }
}
