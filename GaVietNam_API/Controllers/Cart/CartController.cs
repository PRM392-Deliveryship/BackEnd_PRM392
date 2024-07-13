using CoreApiResponse;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Service.Interface;
using GaVietNam_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaVietNam_API.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var cart = await _cartService.GetCart();
                return CustomResult("Data Load Successfully", cart);
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

        [HttpDelete("ClearCart")]
        public IActionResult ClearCart()
        {
            try
            {
                _cartService.ClearCart();
                return CustomResult("Cart cleared.");

            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
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
