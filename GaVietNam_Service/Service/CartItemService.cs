using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using GaVietNam_Repository.Repository;
using GaVietNam_Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartItemResponse> AddItem(CartItemRequest cartItemRequest)
        {
            var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(accountId, out long userId))
            {
                throw new CustomException.ForbbidenException("User ID claim invalid.");
            }

            var kind = _unitOfWork.KindRepository.Get(k => k.Id == cartItemRequest.KindId && k.Status == true).FirstOrDefault();
            if (kind == null)
            {
                throw new CustomException.DataNotFoundException("Kind not found.");
            }

            if (cartItemRequest.Quantity > kind.Quantity)
            {
                throw new CustomException.InvalidDataException("Requested quantity is greater than the available stock.");
            }

            var cart = _unitOfWork.CartRepository.Get(c => c.UserId == userId).FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };
                _unitOfWork.CartRepository.Insert(cart);
                _unitOfWork.Save();
            }

            var chicken = _unitOfWork.ChickenRepository.GetByID(kind.ChickenId);

            var cartItem = _unitOfWork.CartItemRepository.Get(
                    c => c.CartId == cart.Id &&
                    c.KindId == kind.Id,
                    includeProperties: "Kind").FirstOrDefault();
            if (cartItem != null)
            {   
                kind.Quantity -= cartItemRequest.Quantity;
                cartItem.Quantity += cartItemRequest.Quantity;
                cart.TotalPrice += cartItem.Quantity * chicken.Price;

                _unitOfWork.KindRepository.Update(kind);
                _unitOfWork.CartItemRepository.Update(cartItem);
                _unitOfWork.CartRepository.Update(cart);
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    KindId = cartItemRequest.KindId,
                    Quantity = cartItemRequest.Quantity,
                    
                };
                _unitOfWork.CartItemRepository.Insert(cartItem);

                kind.Quantity -= cartItemRequest.Quantity; 
                _unitOfWork.KindRepository.Update(kind);
                cart.TotalPrice += cartItemRequest.Quantity * chicken.Price;
                _unitOfWork.CartRepository.Update(cart);
            }
            _unitOfWork.Save();

            var cartItemResponse = _mapper.Map<CartItemResponse>(cartItem);
            cartItemResponse.Price = cartItem.Quantity*chicken.Price;
            return cartItemResponse;
        }

        public async Task<bool> DeleteItem(long id)
        {
            try
            {
                var cartItem = _unitOfWork.CartItemRepository.Get(
                    ci => ci.Id == id,
                    includeProperties: "Kind").FirstOrDefault();
                if (cartItem == null)
                {
                    throw new CustomException.DataNotFoundException("CartItem not found.");
                }

                _unitOfWork.CartItemRepository.Delete(cartItem);

                var kind = _unitOfWork.KindRepository.GetByID(cartItem.KindId);
                kind.Quantity += cartItem.Quantity;
                _unitOfWork.KindRepository.Update(kind);

                var chicken = _unitOfWork.ChickenRepository.GetByID(kind.ChickenId);

                var cart = _unitOfWork.CartRepository.GetByID(cartItem.CartId);
                cart.TotalPrice -= cartItem.Quantity * chicken.Price;
                _unitOfWork.CartRepository.Update(cart);

                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CartItemResponse> RemoveItem(long id)
        {
            var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(accountId, out long userId))
            {
                throw new CustomException.ForbbidenException("User ID claim invalid.");
            }

            var kind = _unitOfWork.KindRepository.Get(k => k.Id == id && k.Status == true, includeProperties: "Chicken").FirstOrDefault();
            if (kind == null)
            {
                throw new CustomException.DataNotFoundException("Kind not found.");
            }

            var cart = _unitOfWork.CartRepository.Get(c => c.UserId == userId).FirstOrDefault();
            if (cart == null)
            {
                throw new CustomException.DataNotFoundException("Cart not found for the user.");
            }

            var cartItem = _unitOfWork.CartItemRepository.Get(
                ci => ci.CartId == cart.Id &&
                ci.KindId == kind.Id).FirstOrDefault();
            if (cartItem == null)
            {
                throw new CustomException.DataNotFoundException("CartItem with Kind not found");
            }
            cartItem.Quantity--;
            kind.Quantity++;

            cart.TotalPrice = cartItem.Quantity * kind.Chicken.Price;

            await _unitOfWork.CartItemRepository.SaveChangesAsync();
            _unitOfWork.Save();

            var cartItemResponse = _mapper.Map<CartItemResponse>(cartItem);
            cartItemResponse.Price = cartItemResponse.Quantity * kind.Chicken.Price;
            return cartItemResponse;
        }
    }
}
