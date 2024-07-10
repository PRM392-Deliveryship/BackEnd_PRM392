using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Model.DTO.Request
{
    public class RegisterRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string Email { get; set; }
        public string Gender { get; set; }
    }
}
