using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using GaVietNam_Repository.Repository;
using GaVietNam_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Service
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRole(QueryObject queryObject)
        {
            Expression<Func<Role, bool>> filter = null;
            if (!string.IsNullOrWhiteSpace(queryObject.Search))
            {
                filter = r => r.RoleName.Contains(queryObject.Search);
            }

            var roles = _unitOfWork.RoleRepository.Get(
                filter: filter,
                pageIndex: queryObject.PageIndex,
                pageSize: queryObject.PageSize)
                .ToList();

            if (!roles.Any())
            {
                throw new CustomException.DataNotFoundException("No Role in Database");
            }

            var roleResponses = _mapper.Map<IEnumerable<RoleResponse>>(roles);

            return roleResponses;
        }

        public async Task<RoleResponse> GetRoleById(long id)
        {
            var role = _unitOfWork.RoleRepository.GetByID(id);

            if (role == null)
            {
                throw new CustomException.DataNotFoundException($"Role not found with ID: {id}");
            }

            var roleResponses = _mapper.Map<RoleResponse>(role);
            return roleResponses;
        }

        public async Task<RoleResponse> CreateRole(RoleRequest roleRequest)
        {
            var existingRole = _unitOfWork.RoleRepository.Get().FirstOrDefault(p => p.RoleName.ToLower() == roleRequest.RoleName.ToLower());

            if (existingRole != null)
            {
                throw new CustomException.DataExistException($"Kind with ColorName '{roleRequest.RoleName}' already exists.");
            }
            var roleResponse = _mapper.Map<RoleResponse>(existingRole);
            var newRole = _mapper.Map<Role>(roleRequest);

            _unitOfWork.RoleRepository.Insert(newRole);
            _unitOfWork.Save();

            _mapper.Map(newRole, roleResponse);
            return roleResponse;
        }

        public async Task<RoleResponse> UpdateRole(long id, RoleRequest roleRequest)
        {
            var existingRole = _unitOfWork.RoleRepository.GetByID(id);

            if (existingRole == null)
            {
                throw new CustomException.DataNotFoundException($"Role with ID {id} not found.");
            }

            var duplicateExists = _unitOfWork.RoleRepository.Get(p =>
                p.Id != id &&
                p.RoleName.ToLower() == roleRequest.RoleName.ToLower()
            );

            if (duplicateExists == null)
            {
                throw new CustomException.DataExistException($"Role with name '{roleRequest.RoleName}' already exists in Data.");
            }

            _mapper.Map(roleRequest, existingRole);
            _unitOfWork.Save();

            var roleResponse = _mapper.Map<RoleResponse>(existingRole);
            return roleResponse;
        }
        public async Task<bool> DeleteRole(long id)
        {
            try
            {
                var role = _unitOfWork.RoleRepository.GetByID(id);
                if (role == null)
                {
                    throw new CustomException.DataNotFoundException("Role not found.");
                }

                _unitOfWork.RoleRepository.Delete(role);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
