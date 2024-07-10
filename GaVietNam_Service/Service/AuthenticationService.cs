using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using GaVietNam_Repository.Repository;
using GaVietNam_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthenticationService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateAccountDTOResponse> Register(RegisterRequest registerRequest)
        {
            IEnumerable<User> checkEmail =
                await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Email.Equals(registerRequest.Email));
            IEnumerable<User> checkUsername =
                await _unitOfWork.UserRepository.GetByFilterAsync(x => x.Username.Equals(registerRequest.Username));
            if (checkEmail.Count() != 0)
            {
                throw new InvalidDataException($"Email is exist");
            }

            if (checkUsername.Count() != 0)
            {
                throw new InvalidDataException($"Username is exist");
            }

            if (registerRequest.Password != registerRequest.ConfirmPassword)
            {
                throw new CustomException.InvalidDataException("Confirm Password not match!");
            }

            var userGender = "Order";
            switch (registerRequest.Gender.Trim().ToLower())
            {
                case "male": userGender = "Male"; break;
                case "female": userGender = "Female"; break;
            }

            var user = _mapper.Map<User>(registerRequest);
            user.Gender = userGender;
            user.RoleId = 3;
            user.Password = EncryptPassword.Encrypt(registerRequest.Password);
            user.FullName = "";
            user.Avatar = "";
            user.IdentityCard = "";
            user.Dob = DateTime.Now;
            user.Phone = "";    
            user.Status = true;
            user.CreateDate = DateTime.Now.Date;

            await _unitOfWork.UserRepository.AddAsync(user);

            CreateAccountDTOResponse createAccountDTOResponse = _mapper.Map<CreateAccountDTOResponse>(user);
            return createAccountDTOResponse;

        }

        public async Task<(string, LoginDTOResponse)> Login(LoginRequest loginRequest)
        {
            string hashedPass = EncryptPassword.Encrypt(loginRequest.Password);
            IEnumerable<User> check = await _unitOfWork.UserRepository.GetByFilterAsync(x =>
                x.Username.Equals(loginRequest.UserName)
                && x.Password.Equals(hashedPass)
            );
            if (!check.Any())
            {
                throw new CustomException.InvalidDataException(HttpStatusCode.BadRequest.ToString(), $"Username or password error");
            }

            User user = check.First();
            if (user.Status == false)
            {
                throw new CustomException.InvalidDataException(HttpStatusCode.BadRequest.ToString(), $"User is not active");
            }

            LoginDTOResponse loginDtoResponse = _mapper.Map<LoginDTOResponse>(user);
            Authentication authentication = new(_configuration, _unitOfWork);
            string token = await authentication.GenerateJwtToken(user, 15);
            return (token, loginDtoResponse);
        }
    }
}
