using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
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

        public async Task<ChickenResponse> CreateChicken(ChickenRequest chickenRequest)
        {
            var existingChicken = _unitOfWork.ChickenRepository.Get().FirstOrDefault(p => p.Name.ToLower() == chickenRequest.Name.ToLower());

            if (existingChicken != null)
            {
                throw new CustomException.DataExistException($"Chicken with name '{chickenRequest.Name}' already exists.");
            }
            var newChicken = _mapper.Map<Chicken>(chickenRequest);
            newChicken.CreateDate = DateTime.UtcNow;
            newChicken.Status = true;

            var chickenResponse = _mapper.Map<ChickenResponse>(existingChicken);

            _unitOfWork.ChickenRepository.Insert(newChicken);
            _unitOfWork.Save();

            _mapper.Map(newChicken, chickenResponse);
            return chickenResponse;
        }

        public async Task<bool> DeleteChicken(long id)
        {
            try
            {
                var chicken = _unitOfWork.ChickenRepository.GetByID(id);
                if (chicken == null)
                {
                    throw new CustomException.DataNotFoundException("Chicken not found.");
                }

                /*var kinds = _unitOfWork.KindRepository.Get().Where(p => p.ChickenId == id);
                foreach (var kind in kinds)
                {
                    kind.Status = false;
                    _unitOfWork.KindRepository.Update(kind);
                }

                product.Status = false;
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Save();*/

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ChickenResponse>> GetAllChicken(QueryObject queryObject)
        {
            var chickens = _unitOfWork.ChickenRepository.Get(
                filter: p => queryObject.Search == null || p.Name.Contains(queryObject.Search))
                .Where(k => k.Status == true)
                .ToList();
            if (!chickens.Any())
            {
                throw new CustomException.DataNotFoundException("No Chicken in Database");
            }

            var chickenResponses = _mapper.Map<List<ChickenResponse>>(chickens);

            return chickenResponses;
        }

        public async Task<List<ChickenResponse>> GetAllChickenFalse(QueryObject queryObject)
        {
            var chickens = _unitOfWork.ChickenRepository.Get(
                filter: p => queryObject.Search == null || p.Name.Contains(queryObject.Search))
                .Where(k => k.Status == false)
                .ToList();
            if (!chickens.Any())
            {
                throw new CustomException.DataNotFoundException("No Chicken in Database");
            }

            var chickenResponses = _mapper.Map<List<ChickenResponse>>(chickens);

            return chickenResponses;
        }

        public async Task<ChickenResponse> GetChickentById(long id)
        {
            var chicken = _unitOfWork.ChickenRepository.Get(filter: p => p.Id == id).FirstOrDefault();

            if (chicken == null)
            {
                throw new CustomException.DataNotFoundException("Chicken not found");
            }

            var chickenResponse = _mapper.Map<ChickenResponse>(chicken);
            return chickenResponse;
        }

        public async Task<ChickenResponse> UpdateChicken(long id, ChickenRequest chickenRequest)
        {
            throw new NotImplementedException();
        }
    }
}
