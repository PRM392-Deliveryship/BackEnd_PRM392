using CoreApiResponse;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using GaVietNam_Service.Interface;
using GaVietNam_Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaVietNam_API.Controllers.CartItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : BaseController
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPatch("AddItem")]
        [Authorize]
        public async Task<IActionResult> AddItem(CartItemRequest cartItemRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                CartItemResponse cartItemResponse = await _cartItemService.AddItem(cartItemRequest);
                return CustomResult("Add Successful", cartItemResponse, HttpStatusCode.OK);
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

        [HttpPatch("RemoveItem/{id}")]
        [Authorize]
        public async Task<IActionResult> RemoveItem(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return CustomResult(ModelState, HttpStatusCode.BadRequest);
                }

                CartItemResponse cartItemResponse = await _cartItemService.RemoveItem(id);
                return CustomResult("Remove Successful", cartItemResponse, HttpStatusCode.OK);
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

        [HttpDelete("DeleteItemFromCartItem/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteKindFromCartItem(long id)
        {
            try
            {
                var deleteKindFromCartItem = await _cartItemService.DeleteItem(id);
                return CustomResult("Delete Kind from CartItem Successfull", deleteKindFromCartItem, HttpStatusCode.OK);
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
