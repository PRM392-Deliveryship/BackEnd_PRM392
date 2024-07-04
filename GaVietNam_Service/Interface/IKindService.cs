using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GaVietNam_Service.Interface
{
    public interface IKindService
    {
        Task<List<KindResponse>> GetAllKind(QueryObject queryObject);
        Task<List<KindResponse>> GetAllKindFalse(QueryObject queryObject);
        Task<KindResponse> GetKindById(long id);
        Task<List<KindResponse>> GetAllKindByChickenId(long id);
        Task<KindResponse> CreateKind(KindRequest kindRequest);
        Task<KindResponse> UpdateKind(long id, UpdateKindRequest updateKindRequest);
        Task<bool> DeleteKind(long id);
    }
}
