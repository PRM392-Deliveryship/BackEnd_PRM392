using GaVietNam_Model.DTO.Request;
using GaVietNam_Model.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaVietNam_Service.Interface
{
    public interface IOrderService
    {
        //Task<OrderResponse> CreateOrder(OrderRequest orderRequest);

        Task<bool> UpdateStatusOrderConfirmed(long id);

        Task<bool> UpdateStatusOrderReject(long id);

        Task<OrderResponse> GetOrderById(long id);

        List<OrderResponse> GetAllOrderByStatusPending(string? keyword, int pageIndex, int pageSize);

        List<OrderResponse> GetAllOrderByStatusConfirmed(string? keyword, int pageIndex, int pageSize);

        List<OrderResponse> GetAllOrderByStatusReject(string? keyword, int pageIndex, int pageSize);

        Task<double> GetTotalPriceConfirmedOrdersByMonthYear(int month, int year);

        Task<int> CountOrdersConfirmedByMonthYear(int month, int year);

        //Task<List<OrderResponse>> GetOrdersSummaryByMonthYear(int month, int year);
    }
}
