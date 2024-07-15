using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Để region gộp từng cái theo rq và rp

            #region Admin
            CreateMap<AdminRequest, Admin>().ReverseMap();
            CreateMap<Admin, AdminResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<Admin, LoginResponse>();
            #endregion

            #region Chicken
            CreateMap<ChickenRequest, Chicken>().ReverseMap();
            CreateMap<Chicken, ChickenResponse>().ReverseMap();
            #endregion

            #region Kind
            CreateMap<KindRequest, Kind>().ReverseMap();
            CreateMap<UpdateKindRequest, Kind>().ReverseMap();
            CreateMap<Kind, KindResponse>().ReverseMap();
            #endregion

            #region Contact
            CreateMap<CreateContactRequest, Contact>().ReverseMap();
            CreateMap<Contact, ContactResponse>().ReverseMap();
            #endregion

            #region Role
            CreateMap<RoleRequest, Role>().ReverseMap();
            CreateMap<Role,  RoleResponse>().ReverseMap();
            #endregion

            #region User
            CreateMap<CreateAccountDTORequest, User>().ReverseMap();
            CreateMap<UpdateAccountDTORequest, User>().ReverseMap();
            CreateMap<User, UserDTOResponse>().ReverseMap();
            #endregion

            #region Login
            CreateMap<User, LoginDTOResponse>().ReverseMap();
            #endregion

            #region CreateAccount
            CreateMap<DTO.Request.RegisterRequest, User>().ReverseMap();
            CreateMap<CreateAccountDTORequest, User>().ReverseMap();
            CreateMap<User, CreateAccountDTOResponse>().ReverseMap();
            #endregion

            #region Order
            CreateMap<OrderRequest, Order>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
            #endregion

            #region OrderItem
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.ChickenName, opt => opt.MapFrom(src => src.Kind.Chicken.Name))
                .ForMember(dest => dest.KindName, opt => opt.MapFrom(src => src.Kind.KindName))
                .ReverseMap();
            #endregion

            CreateMap<CartItem, OrderItem>().ForMember(dest => dest.Id, opt => opt.Ignore());

            #region Cart
            CreateMap<CartRequest, Cart>().ReverseMap();
            CreateMap<Cart, CartResponse>().ReverseMap();
            #endregion

            #region CartItem
            CreateMap<CartItemRequest, CartItem>().ReverseMap();
            CreateMap<CartItem, CartItemResponse>().ReverseMap();
            #endregion
        }
    }
}
