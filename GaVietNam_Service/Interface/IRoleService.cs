using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllRole(QueryObject queryObject);
        Task<RoleResponse> GetRoleById(long id);
        Task<RoleResponse> CreateRole(RoleRequest roleRequest);
        Task<RoleResponse> UpdateRole(long id, RoleRequest roleRequest);
        Task<bool> DeleteRole(long id);
    }
}
