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
    public class KindService : IKindService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Tools.Firebase _firebase;

        public KindService(IUnitOfWork unitOfWork, IMapper mapper, Tools.Firebase firebase)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebase = firebase;
        }

        public async Task<List<KindResponse>> GetAllKind(QueryObject queryObject)
        {
            var kinds = _unitOfWork.KindRepository.Get(
                filter: p => queryObject.Search == null || p.KindName.Contains(queryObject.Search))
                .Where(k => k.Quantity != 0 && k.Status == true)
                .ToList();
            if (!kinds.Any())
            {
                throw new CustomException.DataNotFoundException("No Kind in Database");
            }

            var kindResponses = _mapper.Map<List<KindResponse>>(kinds);

            return kindResponses;
        }

        public async Task<List<KindResponse>> GetAllKindFalse(QueryObject queryObject)
        {
            var kinds = _unitOfWork.KindRepository.Get(
                filter: p => queryObject.Search == null || p.KindName.Contains(queryObject.Search))
                .Where(k => k.Quantity != 0 && k.Status == false)
                .ToList();

            if (!kinds.Any())
            {
                throw new CustomException.DataNotFoundException("No Kind False in Database");
            }

            var kindResponses = _mapper.Map<List<KindResponse>>(kinds);

            return kindResponses;
        }

        public async Task<List<KindResponse>> GetAllKindByChickenId(long id)
        {
            var product = _unitOfWork.ChickenRepository.GetByID(id);

            if (product == null)
            {
                throw new CustomException.DataNotFoundException($"Chicken not found with ID: {id}");
            }

            var kinds = _unitOfWork.KindRepository.Get(
                filter: k => k.ChickenId == id && k.Status == true).ToList();
            if (!kinds.Any())
            {
                throw new CustomException.DataNotFoundException($"Chicken with ID: {id} does not have any Kind");
            }

            var kindResponse = _mapper.Map<List<KindResponse>>(kinds);
            return kindResponse;
        }

        public async Task<KindResponse> GetKindById(long id)
        {
            var kind = _unitOfWork.KindRepository.Get(filter: p => p.Id == id).FirstOrDefault();

            if (kind == null)
            {
                throw new CustomException.DataNotFoundException("Kind not found");
            }

            var kindResponse = _mapper.Map<KindResponse>(kind);
            return kindResponse;
        }

        public async Task<KindResponse> CreateKind(KindRequest kindRequest)
        {
            var existingKind = _unitOfWork.KindRepository.Get().FirstOrDefault(p => p.ChickenId == kindRequest.ChickenId &&
                                                                p.KindName.ToLower() == kindRequest.KindName.ToLower());

            if (existingKind != null)
            {
                throw new CustomException.DataExistException($"Kind with name '{kindRequest.KindName}' already exists.");
            }

            var chicken = _unitOfWork.ChickenRepository.GetByID(kindRequest.ChickenId);
            if (chicken == null)
            {
                throw new CustomException.DataNotFoundException("Chicken not found.");
            }

            chicken.Stock += kindRequest.Quantity;

            var kindResponse = _mapper.Map<KindResponse>(existingKind);
            var newKind = _mapper.Map<Kind>(kindRequest);

            if (kindRequest.File != null)
            {
                if (kindRequest.File.Length >= 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }
                string imageDownloadUrl = await _firebase.UploadImage(kindRequest.File);
                newKind.Image = imageDownloadUrl;
            }


            newKind.Status = true;
            _unitOfWork.KindRepository.Insert(newKind);
            _unitOfWork.Save();

            _mapper.Map(newKind, kindResponse);
            return kindResponse;
        }

        public async Task<KindResponse> UpdateKind(long id, UpdateKindRequest updateKindRequest)
        {
            var existingKind = _unitOfWork.KindRepository.GetByID(id);

            if (existingKind == null)
            {
                throw new CustomException.DataNotFoundException($"Kind with ID {id} not found.");
            }

            if (!existingKind.Status)
            {
                throw new CustomException.InvalidDataException($"Kind with ID {id} was InActive.");
            }

            // Check for duplicates (excluding the current Kind being updated)
            var duplicateExists = _unitOfWork.KindRepository.Exists(p =>
                p.Id != id &&
                p.ChickenId == existingKind.ChickenId &&
                p.KindName.ToLower() == updateKindRequest.KindName.ToLower()
            );

            if (duplicateExists)
            {
                throw new CustomException.DataExistException($"Kind with name '{updateKindRequest.KindName}' already exists for this product.");
            }

            var chicken = _unitOfWork.ChickenRepository.GetByID(existingKind.ChickenId);

            int change = updateKindRequest.Quantity - existingKind.Quantity;
            chicken.Stock += change;

            if (updateKindRequest.File != null)
            {
                if (updateKindRequest.File.Length >= 10 * 1024 * 1024)
                {
                    throw new CustomException.InvalidDataException("File size exceeds the maximum allowed limit.");
                }

                string imageDownloadUrl = await _firebase.UploadImage(updateKindRequest.File);

                if (!string.IsNullOrEmpty(imageDownloadUrl))
                {
                    existingKind.Image = imageDownloadUrl;
                }
            }
            _unitOfWork.ChickenRepository.Update(chicken);
            _mapper.Map(updateKindRequest, existingKind);

            _unitOfWork.Save(); // Save all changes (existingKind and product) together

            var kindResponse = _mapper.Map<KindResponse>(existingKind);
            return kindResponse;
        }

        public async Task<bool> DeleteKind(long id)
        {
            try
            {
                var kind = _unitOfWork.KindRepository.GetByID(id);
                if (kind == null)
                {
                    throw new CustomException.DataNotFoundException("Kind not found.");
                }

                var chicken = _unitOfWork.ChickenRepository.GetByID(kind.ChickenId);
                if (chicken == null)
                {
                    throw new CustomException.DataNotFoundException("Chicken not found.");
                }

                chicken.Stock -= kind.Quantity;
                kind.Status = false;
                _unitOfWork.KindRepository.Update(kind);
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
