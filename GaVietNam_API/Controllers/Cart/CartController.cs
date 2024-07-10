using CoreApiResponse;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Service.Interface;
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

        [HttpGet("GetCartItems")]
        public IActionResult GetCartItems()
        {
            var items = _cartService.GetCartItems();
            return CustomResult("View chicken from cart.", items);
        }

        [HttpPost("AddChickenToCart")]
        public IActionResult AddItemToCart([FromBody] CartRequest request)
        {
            try
            {
                var cartItemDTO = _cartService.AddItem(request.Id, request.Quantity);
                return CustomResult("Chicken added to cart.", cartItemDTO);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult("An error occurred while adding chicken to the cart.", HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("RemoveChickenFromCart/{chickenId}")]
        public IActionResult RemoveItemFromCart(long chickenId)
        {
            _cartService.RemoveItem(chickenId);
            return CustomResult("Chicken removed from cart.");
        }

        [HttpPut("UpdateChickenInCart")]
        public IActionResult UpdateItemQuantityInCart([FromBody] CartRequest request)
        {
            try
            {
                _cartService.UpdateItemQuantity(request.Id, request.Quantity);
                return CustomResult("Chicken quantity updated successfully.");
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult("An error occurred while updating the chicken quantity.", HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("ClearCart")]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return CustomResult("Cart cleared.");
        }
    }
}
