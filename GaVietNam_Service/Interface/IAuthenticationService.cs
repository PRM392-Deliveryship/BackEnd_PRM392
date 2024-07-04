using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(string Token, LoginResponse loginResponse)> AuthorizeUser(LoginRequest loginRequest);
    }
}
