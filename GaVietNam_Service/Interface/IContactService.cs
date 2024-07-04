using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Service.Interface
{
    public interface IContactService
    {
        Task<ContactResponse> CreateContact(CreateContactRequest createContactRequest);
        Task<ContactResponse> DeleteContact(long id);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(long id);
        Task<ContactResponse> UpdateContact(long id, UpdateContactRequest updateContactRequest);
    }
}
