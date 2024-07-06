using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using GaVietNam_Repository.Repository;
using GaVietNam_Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Tools.Firebase _firebase;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, Tools.Firebase firebase)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _firebase = firebase;
        }
        public async Task<User> CreateUser(CreateAccountDTORequest createAccountRequest)
        {
            IEnumerable<User> checkEmail =
                await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Email.Equals(createAccountRequest.Email));
            IEnumerable<User> checkUsername =
                await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Username.Equals(createAccountRequest.Username));
            if (checkEmail.Count() != 0)
            {
                throw new InvalidDataException($"Email is exist");
            }

            if (checkUsername.Count() != 0)
            {
                throw new InvalidDataException($"Username is exist");
            }

            var user = _mapper.Map<User>(createAccountRequest);
            user.Password = EncryptPassword.Encrypt(createAccountRequest.Password);
            user.Status = true;
            user.CreateDate = DateTime.Now;
            user.Avatar = null;
            await _unitOfWork.UserRepository.AddAsync(user);
            return user;
        }

        public async Task<IEnumerable<UserDTOResponse>> GetAllUsers(QueryObject queryObject)
        {
            //check if QueryObject search is not null
            Expression<Func<User, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(queryObject.Search))
            {
                filter = user => user.Username.Contains(queryObject.Search);
            }

            var users = _unitOfWork.UserRepository.Get(
                filter: filter,
                includeProperties: "Role",
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize);
            if (users.IsNullOrEmpty())
            {
                throw new CustomException.DataNotFoundException("The user list is empty!");
            }

            return _mapper.Map<IEnumerable<UserDTOResponse>>(users);
        }

        public async Task<User> GetUserById(long id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task<User> UpdateUser(long id, UpdateAccountDTORequest updateAccountDTORequest)
        {
            var userToUpdate = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                throw new InvalidDataException($"User not found");
            }
            _mapper.Map(updateAccountDTORequest, userToUpdate);
            await _unitOfWork.UserRepository.UpdateAsync(userToUpdate);

            return userToUpdate;
        }

        public string GetUserID()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                var claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
                if (claim != null)
                {
                    result = claim.Value;
                }
            }
            return result;
        }

        public async Task<UserDTOResponse> GetLoginUser()
        {
            var user = GetUserFromHttpContext();


            return _mapper.Map<UserDTOResponse>(user);
        }

        public async Task<UserDTOResponse> UpdateLoginUser(UpdateAccountDTORequest updateAccountDTORequest)
        {

            var user = GetUserFromHttpContext();

            _mapper.Map(updateAccountDTORequest, user);

            IEnumerable<User> checkEmail =
                await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Email.Equals(updateAccountDTORequest.Email));
            if (checkEmail.Count() != 0)
            {
                throw new InvalidDataException($"Email is exist");
            }
            var userGender = "Order";
            switch (updateAccountDTORequest.Gender.Trim().ToLower())
            {
                case "male": userGender = "Male"; break;
                case "female": userGender = "Female"; break;
            }

            user.Gender = userGender;
            await _unitOfWork.UserRepository.UpdateAsync(user);
            _unitOfWork.Save();

            return _mapper.Map<UserDTOResponse>(user);
        }

        public async Task<UserDTOResponse> UpdateLoginUserAvatar(ImageRequest imageRequest)
        {
            var user = GetUserFromHttpContext();
            string[] imgExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            if (!imgExtensions.Contains(Path.GetExtension(imageRequest.Image.FileName)))
            {
                throw new CustomException.InvalidDataException("Just accept image!");
            }
            var imageurl = await _firebase.UploadImage(imageRequest.Image);
            user.Avatar = imageurl;
            _unitOfWork.Save();
            return _mapper.Map<UserDTOResponse>(user);
        }

        private User GetUserFromHttpContext()
        {
            var userId = long.Parse(Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext));
            var user = _unitOfWork.UserRepository.Get(u => u.Id == userId, includeProperties: "Role").FirstOrDefault();
            if (user == null)
            {
                throw new CustomException.DataNotFoundException("This user not found!");
            }

            if (user.Status == false)
            {
                throw new CustomException.InvalidDataException("This user not activated!");

            }

            return user;
        }
    }
}
