using AutoMapper;
using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using GaVietNam_Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Model.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Để region gộp từng cái theo rq và rp

            #region Chicken
            CreateMap<ChickenRequest, Chicken>().ReverseMap();
            CreateMap<Chicken, ChickenResponse>().ReverseMap();
            #endregion

        }
    }
}
