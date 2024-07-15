using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Service.Interface
{
    public interface ICartService
    {
        Task<List<CartResponse>> GetCart();

        void ClearCart();
    }
}
