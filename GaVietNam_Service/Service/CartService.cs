using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using GaVietNam_Repository.Repository;
using GaVietNam_Service.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Service
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartResponse> GetCart()
        {
            var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(accountId, out long userId))
            {
                throw new CustomException.ForbbidenException("User ID claim invalid.");
            }

            var cart = _unitOfWork.CartRepository.Get(c => c.UserId == userId, 
                includeProperties: "User,CartItems,CartItems.Kind.Chicken,CartItems.Kind").FirstOrDefault();
            if (cart == null)
            {
                throw new CustomException.DataNotFoundException("Cart not found");
            }

            var cartResponse = _mapper.Map<CartResponse>(cart);
            return cartResponse;
        }

        public void ClearCart()
        {
            var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            if (!long.TryParse(accountId, out long userId))
            {
                throw new CustomException.ForbbidenException("User ID claim invalid.");
            }

            var cart = _unitOfWork.CartRepository.Get(c => c.UserId == userId).FirstOrDefault();
            if (cart == null)
            {
                throw new CustomException.DataNotFoundException("Cart not found for the user.");
            }

            var existCartItem = _unitOfWork.CartItemRepository.Get(
                ci => ci.CartId == cart.Id);
            if (existCartItem == null)
            {
                throw new CustomException.DataNotFoundException("No CartItem in Cart");
            }

            _unitOfWork.CartItemRepository.Delete(existCartItem);
            cart.TotalPrice = 0;
            _unitOfWork.CartRepository.Update(cart);
            _unitOfWork.Save();

        }
    }
}
