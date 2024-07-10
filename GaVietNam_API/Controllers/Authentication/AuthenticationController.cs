using CoreApiResponse;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
             try
             {
                CreateAccountDTOResponse user = await _authenticationService.Register(registerRequest);

                return CustomResult("Register Success",user, HttpStatusCode.OK);

             }
             catch (Exception e)
             {
                  return CustomResult(e.Message, HttpStatusCode.InternalServerError);
             }
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
        {
            try
            {
                (string, LoginDTOResponse) tuple = await _authenticationService.Login(loginRequest);
                if (tuple.Item1 == null)
                {
                    return Unauthorized();
                }

                Dictionary<string, object> result = new()
                {
                    { "token", tuple.Item1 },
                    { "user", tuple.Item2 ?? null }
                };
                return CustomResult("Login Success", result, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return CustomResult(e.Message, HttpStatusCode.InternalServerError);
            }


        }
    }
}
