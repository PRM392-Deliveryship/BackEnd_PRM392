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
    public interface IChickenService
    {
        Task<List<ChickenResponse>> GetAllChicken(QueryObject queryObject);
        Task<List<ChickenResponse>> GetAllChickenFalse(QueryObject queryObject);
        Task<ChickenResponse> CreateChicken(ChickenRequest chickenRequest);
        Task<ChickenResponse> UpdateChicken(long id, ChickenRequest chickenRequest);
        Task<bool> DeleteChicken(long id);
        Task<ChickenResponse> GetChickentById(long id);
    }
}
