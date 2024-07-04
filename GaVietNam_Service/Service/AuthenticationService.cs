using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Repository;
using GaVietNam_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<(string Token, LoginResponse loginResponse)> AuthorizeUser(LoginRequest loginRequest)
        {

            Authentication authentication = new(_configuration, _unitOfWork);

            var member = _unitOfWork.AdminRepository
                .Get(filter: a => a.Username == loginRequest.UserName && a.Status == true).FirstOrDefault();
            if (member != null && authentication.VerifyPassword(loginRequest.Password, member.Password))
            {
                string token = authentication.GenerateToken(member);
                var adminResponse = _mapper.Map<LoginResponse>(member);
                return (token, adminResponse);
            }
            return (null, null);
        }
    }
}
