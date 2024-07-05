using CoreApiResponse;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaVietNam_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("getAllAdminByStatusTrue")]
        public IActionResult GetAllAdminByStatusTrue()
        {
            var admin = _adminService.GetAllAdminByStatusTrue();
            return CustomResult("Data load Successful", admin);
        }

        [HttpGet("getAllAdminByStatusFalse")]
        [Authorize]
        public IActionResult GetAllAdminByStatusFalse()
        {
            try
            {
                var admin = _adminService.GetAllAdminByStatusFalse();
                return CustomResult("Data load Successful", admin);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet("getAdminById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAdminById(long id)
        {
            try
            {
                var admin = await _adminService.GetAdminById(id);

                return CustomResult("Create admin successful", admin);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("createAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRequest adminRequest)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            var result = await _adminService.CreateAdmin(adminRequest);

            if (!result.Status)
            {
                return CustomResult("Create fail.", new { userName = result.UserName }, HttpStatusCode.Conflict);
            }

            return CustomResult("Create Successful", result);

        }

        [HttpPatch("updateAdmin/{id}")]
        public async Task<IActionResult> UpdateAdmin(long id, [FromBody] AdminRequest adminRequest)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            try
            {
                var result = await _adminService.UpdateAdmin(id, adminRequest);
                return CustomResult("Update Successful", result);
            }
            catch (Exception ex)
            {
                return CustomResult("Update Admin Fail", HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("deletetAdmin/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAdmin(long id)
        {
            try
            {
                var result = await _adminService.DeleteAdmin(id);
                return CustomResult("Delete Successful.");
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
