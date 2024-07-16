using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using GaVietNam_Repository.Repository;
using GaVietNam_Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
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
    public class AdminService : IAdminService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<AdminResponse> GetAllAdminByStatusTrue()
        {
            var listAdmin = _unitOfWork.AdminRepository.Get(
                filter: s => s.Status == true, 
                includeProperties: "Role"
            ).ToList();
            var adminResponses = _mapper.Map<IEnumerable<AdminResponse>>(listAdmin);
            return adminResponses;
        }

        public IEnumerable<AdminResponse> GetAllAdminByStatusFalse()
        {
            try
            {
                var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(accountId))
                {
                    throw new CustomException.ForbbidenException("User ID claim invalid.");
                }

                if (!long.TryParse(accountId, out long Id))
                {
                    throw new CustomException.ForbbidenException("User ID claim invalid.");
                }

                var checkmanager = _unitOfWork.AdminRepository.Get(a => a.Id == Id).FirstOrDefault();

                var listAdmin = _unitOfWork.AdminRepository.Get(
                    filter: s => s.Status == true,
                    includeProperties: "Role"
                ).ToList();
                var adminResponses = _mapper.Map<IEnumerable<AdminResponse>>(listAdmin);
                return adminResponses;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException("An error occurred during data processing.", ex);
            }
        }

        public async Task<AdminResponse> GetAdminById(long id)
        {
            try
            {
                var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(accountId))
                {
                    throw new CustomException.ForbbidenException("User ID claim invalid.");
                }

                if (!long.TryParse(accountId, out long Id))
                {
                    throw new CustomException.ForbbidenException("User ID claim invalid.");
                }

                var checkmanager = _unitOfWork.AdminRepository.Get(a => a.Id == Id).FirstOrDefault();

                var admin = _unitOfWork.AdminRepository.Get(
                    filter: a => a.Id == id && a.Status == true, includeProperties: "Role"
                ).FirstOrDefault();

                if (admin == null)
                {
                    throw new CustomException.DataNotFoundException("Admin not found.");
                }

                var adminResponse = _mapper.Map<AdminResponse>(admin);
                return adminResponse;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException("An error occurred during data processing.", ex);
            }
        }



        public async Task<AdminResponse> CreateAdmin(AdminRequest adminRequest)
        {
            try
            {
                Authentication authentication = new(_configuration, _unitOfWork);

                bool usernameExists = _unitOfWork.AdminRepository.Exists(a => a.Username == adminRequest.UserName);
                if (usernameExists)
                {
                    return new AdminResponse
                    {
                        UserName = "Username already exists.",
                        Status = false
                    };
                }

                var admin = _mapper.Map<Admin>(adminRequest);

                admin.Password = EncryptPassword.Encrypt(adminRequest.Password);
                admin.RoleId = 1;
                admin.Status = true;

                _unitOfWork.AdminRepository.Insert(admin);
                _unitOfWork.Save();

                var adminResponse = _mapper.Map<AdminResponse>(admin);
                return adminResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(string, LoginResponse)> LoginAdmin(GaVietNam_Model.DTO.Request.LoginRequest loginRequest)
        {
            string hashedPass = EncryptPassword.Encrypt(loginRequest.Password);
            IEnumerable<Admin> check = await _unitOfWork.AdminRepository.GetByFilterAsync(x =>
                x.Username.Equals(loginRequest.UserName)
                && x.Password.Equals(hashedPass)
            );
            if (!check.Any())
            {
                throw new CustomException.InvalidDataException(HttpStatusCode.BadRequest.ToString(), $"Username or password error");
            }

            Admin admin = check.First();
            if (admin.Status == false)
            {
                throw new CustomException.InvalidDataException(HttpStatusCode.BadRequest.ToString(), $"User is not active");
            }

            LoginResponse loginResponse = _mapper.Map<LoginResponse>(admin);
            Authentication authentication = new(_configuration, _unitOfWork);
            string token = authentication.GenerateToken(admin);
            return (token, loginResponse);
        }

        public async Task<AdminResponse> UpdateAdmin(long id, AdminRequest adminRequest)
        {
            try
            {
                Authentication authentication = new(_configuration, _unitOfWork);

                var existingAdmin = _unitOfWork.AdminRepository.GetByID(id);

                if (existingAdmin == null)
                {
                    throw new CustomException.DataNotFoundException("Admin not found.");
                }

                var admin = _mapper.Map(adminRequest, existingAdmin);

                admin.Password = authentication.HashPassword(adminRequest.Password);

                _unitOfWork.AdminRepository.Update(existingAdmin);
                _unitOfWork.Save();

                var adminResponse = _mapper.Map<AdminResponse>(existingAdmin);
                return adminResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAdmin(long id)
        {
            try
            {
                var accountId = Authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(accountId))
                {
                    throw new CustomException.ForbbidenException("User ID claim invalid.");
                }

                if (!long.TryParse(accountId, out long Id))
                {
                    throw new CustomException.ForbbidenException("User ID claim invalid.");
                }

                var checkmanager = _unitOfWork.AdminRepository.Get(a => a.Id == Id).FirstOrDefault();

                var admin = _unitOfWork.AdminRepository.GetByID(id);
                if (admin == null)
                {
                    throw new CustomException.DataNotFoundException("Admin not found.");
                }

                admin.Status = false;
                _unitOfWork.AdminRepository.Update(admin);
                _unitOfWork.Save();

                return true;
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                throw new CustomException.InternalServerErrorException("An error occurred during data processing.", ex);
            }
        }
    }
}
