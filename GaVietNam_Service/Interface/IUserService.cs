using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Interface
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateAccountDTORequest createAccountRequest);
        Task<IEnumerable<UserDTOResponse>> GetAllUsers(QueryObject queryObject);
        Task<User> GetUserById(long id);
        Task<User> UpdateUser(long id, UpdateAccountDTORequest updateAccountDTORequest);
        string GetUserID();
        Task<UserDTOResponse> GetLoginUser();

        Task<UserDTOResponse> UpdateLoginUser(UpdateAccountDTORequest updateAccountDTORequest);

        Task<UserDTOResponse> UpdateLoginUserAvatar(ImageRequest imageRequest);
    }
}
