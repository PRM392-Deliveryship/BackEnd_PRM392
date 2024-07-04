using CoreApiResponse;
using GaVietNam_Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaVietNam_API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /*[HttpGet("GetLoginUser")]
        public async Task<IActionResult> GetLoginUser()
        {
            try
            {
                var user = await _userService.GetLoginUser();
                return CustomResult("Get User Success", user, HttpStatusCode.OK);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("UpdateLoginUser")]
        public async Task<IActionResult> UpdateLoginUser([FromBody] UpdateAccountDTORequest updateAccountDtoRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                var user = await _userService.UpdateLoginUser(updateAccountDtoRequest);
                return CustomResult("Update Successful!", user);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPatch("UpdateLoginUserAvatar")]

        public async Task<IActionResult> UpdateLoginUserAvatar([FromForm] ImageRequest imageRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                var users = await _userService.UpdateLoginUserAvatar(imageRequest);
                return CustomResult("Change Avatar successful", users);

            }
            catch (Exception exception)
            {
                return CustomResult(exception.Message, HttpStatusCode.InternalServerError);
            }

        }*/
    }
}
