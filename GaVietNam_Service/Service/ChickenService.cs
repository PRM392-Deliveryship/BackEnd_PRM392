using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Repository;
using GaVietNam_Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Service
{
    public class ChickenService : IChickenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChickenService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ChickenResponse> CreateChicken(ChickenRequest chickenRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteChicken(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChickenResponse>> GetAllChicken(QueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChickenResponse>> GetAllChickenFalse(QueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<ChickenResponse> GetChickentById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ChickenResponse> UpdateChicken(long id, ChickenRequest chickenRequest)
        {
            throw new NotImplementedException();
        }
    }
}
