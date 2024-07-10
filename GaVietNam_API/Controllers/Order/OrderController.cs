using CoreApiResponse;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Repository.Entity;
using GaVietNam_Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaVietNam_API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var result = await _orderService.CreateOrder(orderRequest);

                return CustomResult("Create Successful", result);
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

        [HttpPatch("UpdateStatusOrderConfirmed/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatusOrderConfirmed(long id)
        {

            try
            {
                var result = await _orderService.UpdateStatusOrderConfirmed(id);
                return CustomResult("Confirmed Order Successful", result);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("UpdateStatusOrderReject/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatusOrderReject(long id)
        {

            try
            {
                var result = await _orderService.UpdateStatusOrderReject(id);
                return CustomResult("Reject Order Successful", result);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet("GetOrderByStatusPending")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllOrderByStatusPending(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetAllOrderByStatusPending(keyword, pageIndex, pageSize);
                return CustomResult("Data load Successful", order);
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

        [HttpGet("GetOrderByStatusConfirmed")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllOrderByStatusConfirmed(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetAllOrderByStatusConfirmed(keyword, pageIndex, pageSize);
                return CustomResult("Data load Successful", order);
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

        [HttpGet("GetOrderByStatusRejected")]
        [Authorize(Roles = "Admin")]
        public IActionResult getAllOrderByStatusReject(string? keyword, int pageIndex, int pageSize)
        {
            try
            {
                var order = _orderService.GetAllOrderByStatusReject(keyword, pageIndex, pageSize);
                return CustomResult("Data load Successful", order);
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

        [HttpGet("GetOrderById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderById(long id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);

                return CustomResult("Get Order successful.", order);
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

        [HttpGet("TotalPriceConfirmedOrder")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalPriceConfirmedOrdersByMonthYear([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var totalAmount = await _orderService.GetTotalPriceConfirmedOrdersByMonthYear(month, year);
                return CustomResult("Get successfull.", totalAmount);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("CountConfirmedOrders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CountConfirmedOrdersByMonthYear(int month, int year)
        {
            try
            {
                var count = await _orderService.CountOrdersConfirmedByMonthYear(month, year);
                return Ok(new { Count = count });
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        /*[HttpGet("GetOrdersSummaryByMonthYear")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrdersSummaryByMonthYear(int month, int year)
        {
            try
            {
                var count = await _orderService.GetOrdersSummaryByMonthYear(month, year);
                return Ok(count);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }*/
    }
}
