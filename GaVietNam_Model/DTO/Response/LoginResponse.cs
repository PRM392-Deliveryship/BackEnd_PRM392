using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.DTO.Response
{
    public class LoginResponse
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string UserName { get; set; }
    }
}
