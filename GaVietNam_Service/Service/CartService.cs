using AutoMapper;
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

        public CartResponse GetCartItems()
        {
            var cartItems = GetCartItemsFromSession();
            var cartItemDTOs = cartItems.Select(item => _mapper.Map<CartItem>(item)).ToList();
            var totalPrice = cartItemDTOs.Sum(item => item.ChickenPrice * item.Quantity);
            return new CartResponse { Items = cartItemDTOs, TotalPrice = totalPrice };
        }

        public CartItem AddItem(long id, int quantity)
        {
            var chickenKind = _unitOfWork.KindRepository.GetByID(id);
            if (chickenKind == null)
            {
                throw new CustomException.DataNotFoundException("Chicken not found.");
            }

            if (quantity > chickenKind.Quantity)
            {
                throw new CustomException.InvalidDataException("Requested quantity is greater than the available stock.");
            }
            var chicken = _unitOfWork.ChickenRepository.GetByID(chickenKind.ChickenId);

            var cartItems = GetCartItemsFromSession();
            var existingItem = cartItems.FirstOrDefault(item => item.Id == id);
            if (existingItem != null)
            {
                if (existingItem.Quantity + quantity > chickenKind.Quantity)
                {
                    throw new CustomException.InvalidDataException("Requested quantity exceeds the available stock.");
                }
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem { Id = id, Quantity = quantity, ChickenPrice = chicken.Price, ChickenName = chicken.Name, KindImage = chickenKind.Image, KindName = chickenKind.KindName });
            }
            SaveCartItemsToSession(cartItems);
            return _mapper.Map<CartItem>(existingItem ?? cartItems.Last());
        }

        public void UpdateItemQuantity(long id, int quantity)
        {
            var cartItems = GetCartItemsFromSession();
            var item = cartItems.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                var chickenKind = _unitOfWork.KindRepository.GetByID(id);
                if (chickenKind == null)
                {
                    throw new CustomException.DataNotFoundException("Chicken kind not found.");
                }

                var product = _unitOfWork.ChickenRepository.GetByID(chickenKind.ChickenId);
                if (product == null)
                {
                    throw new CustomException.DataNotFoundException("Chicken not found.");
                }

                if (quantity > chickenKind.Quantity)
                {
                    throw new CustomException.InvalidDataException("Requested quantity exceeds the available stock.");
                }

                item.Quantity = quantity;
                SaveCartItemsToSession(cartItems);
            }
            else
            {
                throw new CustomException.DataNotFoundException("Item not found in cart.");
            }
        }

        public void RemoveItem(long id)
        {
            throw new NotImplementedException();
        }

        public void ClearCart()
        {
            var cartItems = new List<CartItem>();
            SaveCartItemsToSession(cartItems);
        }

        private List<CartItem> GetCartItemsFromSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartItemsJson = session.GetString("CartItems");
            return cartItemsJson == null ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartItemsJson);
        }

        private void SaveCartItemsToSession(List<CartItem> cartItems)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartItemsJson = JsonConvert.SerializeObject(cartItems);
            session.SetString("CartItems", cartItemsJson);
        }
    }
}
