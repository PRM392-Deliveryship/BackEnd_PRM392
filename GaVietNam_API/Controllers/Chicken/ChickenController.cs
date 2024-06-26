using CoreApiResponse;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaVietNam_API.Controllers.Chicken
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChickenController : BaseController
    {
        private readonly IChickenService _chickenService;

        public ChickenController(IChickenService chickenService)
        {
            _chickenService = chickenService;
        }

        [HttpGet("GetAllChicken")]
        public IActionResult GetAllChicken([FromQuery] QueryObject queryObject)
        {
            try
            {
                var chickens = _chickenService.GetAllChicken(queryObject);
                return CustomResult("Get all data Successfully", chickens);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetAllChickenFalse")]
        public IActionResult GetAllChickenFalse([FromQuery] QueryObject queryObject)
        {
            try
            {
                var chickens = _chickenService.GetAllChickenFalse(queryObject);
                return CustomResult("Get all data Successfully", chickens);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetChickentById/{id}")]
        public async Task<IActionResult> GetChickentById(long id)
        {
            try
            {
                var chicken = await _chickenService.GetChickentById(id);

                return CustomResult("Chicken is found", chicken);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("CreateChicken")]
        public async Task<IActionResult> CreateChicken([FromBody] ChickenRequest chickenRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                ChickenResponse chicken = await _chickenService.CreateChicken(chickenRequest);
                return CustomResult("Create Successful", chicken, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpPatch("UpdateChicken/{id}")]
        public async Task<IActionResult> UpdateChicken(long id, [FromBody] ChickenRequest chickenRequest)
        {
            try
            {
                ChickenResponse chicken = await _chickenService.UpdateChicken(id, chickenRequest);
                return CustomResult("updated Successful", chicken, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.DataExistException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("DeleteChicken/{id}")]
        public async Task<IActionResult> DeleteChicken(long id)
        {
            try
            {
                var deleteChicken = await _chickenService.DeleteChicken(id);
                return CustomResult("Delete Chicken Successfull (Status)", deleteChicken, HttpStatusCode.OK);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }
    }
}
