using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Service.Interface
{
    public interface IAdminService
    {

        IEnumerable<AdminResponse> GetAllAdminByStatusTrue();

        IEnumerable<AdminResponse> GetAllAdminByStatusFalse();

        Task<AdminResponse> CreateAdmin(AdminRequest adminRequest);
        Task<AdminResponse> UpdateAdmin(long id, AdminRequest adminRequest);
        Task<bool> DeleteAdmin(long adminId);
        Task<AdminResponse> GetAdminById(long id);
    }
}
