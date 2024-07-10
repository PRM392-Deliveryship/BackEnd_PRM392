using GaVietNam_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Service.Interface
{
    public interface IEmailService
    {
        Task SendConfirmedOrderEmailAsync(string toEmail, OrderResponse orderResponse);
        Task SendRejectedOrderEmailAsync(string toEmail, OrderResponse orderResponse);
    }
}
