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

namespace GaVietNam_Service.Service
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {

            return _unitOfWork.ContactRepository.Get();
        }

        public async Task<Contact> GetContactById(long id)
        {
            return _unitOfWork.ContactRepository.GetByID(id);
        }

        public async Task<ContactResponse> CreateContact(CreateContactRequest createContactRequest)
        {
            var createcontact = _mapper.Map<Contact>(createContactRequest);
            _unitOfWork.ContactRepository.Insert(createcontact);

            ContactResponse createContactReponse = _mapper.Map<ContactResponse>(createcontact);
            return createContactReponse;
        }

        public async Task<ContactResponse> UpdateContact(long id, UpdateContactRequest updateContactRequest)
        {
            var existcontact = _unitOfWork.ContactRepository.GetByID(id);
            if (existcontact == null)
            {
                throw new Exception("ID isn't exist");
            }
            //map với cái biến đang có giá trị id
            _mapper.Map(updateContactRequest, existcontact);

            _unitOfWork.ContactRepository.Update(existcontact);
            _unitOfWork.Save();
            var contactresponse = _mapper.Map<ContactResponse>(existcontact);
            return contactresponse;
        }

        public async Task<ContactResponse> DeleteContact(long id)
        {
            var deletesubcription = _unitOfWork.ContactRepository.GetByID(id);
            if (deletesubcription == null)
            {
                throw new Exception("Subcription ID is not exist");
            }
            _unitOfWork.ContactRepository.Delete(deletesubcription);

            //map vào giá trị response
            var contactresponse = _mapper.Map<ContactResponse>(deletesubcription);
            return contactresponse;
        }
    }
}
