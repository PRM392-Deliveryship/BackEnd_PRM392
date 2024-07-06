using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Model.DTO.Request
{
    public class CreateAccountDTORequest
    {
        [StringLength(maximumLength: 40, MinimumLength = 8)]
        public required string Fullname { get; set; }
        [StringLength(maximumLength: 40, MinimumLength = 8)]
        public required string Username { get; set; }
        [CustomDataValidation.PasswordValidation]
        public required string Password { get; set; }
        [CustomDataValidation.EmailValidation]
        public required string Email { get; set; }
        public IFormFile? Avatar { get; set; }
        public string Gender { get; set; }
        public string RoleName { get; set; }
        [RegularExpression("^0(0[1-9]|[1-8][0-9]|9[0-6])[0-3]([0-9][0-9])[0-9]{6}$", ErrorMessage = "CMND not found")]
        [Required(ErrorMessage = "CMND is required.")]
        public string IdentityCard { get; set; }
        [RegularExpression("^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$", ErrorMessage = "This Phone number is fake!")]
        [Required(ErrorMessage = "Phone number is required.")]
        public string phone { get; set; }
    }
}
