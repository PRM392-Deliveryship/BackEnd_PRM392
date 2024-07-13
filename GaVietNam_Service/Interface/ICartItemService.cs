using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Service.Interface
{
    public interface ICartItemService
    {
        Task<CartItemResponse> AddItem(CartItemRequest cartItemRequest);
        Task<CartItemResponse> RemoveItem(long id);
        Task<bool> DeleteItem(long id);
    }
}
