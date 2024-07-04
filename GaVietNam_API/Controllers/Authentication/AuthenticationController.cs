using CoreApiResponse;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaVietNam_API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authenticationService.AuthorizeUser(loginRequest);
            if (result.Token != null)
            {
                return CustomResult("Login successful.", new { result.Token, LoginResponse = result.loginResponse });
            }
            else
            {
                return CustomResult("Invalid email or password.", HttpStatusCode.Unauthorized);
            }
        }
    }
}
