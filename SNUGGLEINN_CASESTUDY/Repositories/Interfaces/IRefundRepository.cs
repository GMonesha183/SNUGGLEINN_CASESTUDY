using System.Collections.Generic;
using System.Threading.Tasks;
using SNUGGLEINN_CASESTUDY.Models;

namespace SNUGGLEINN_CASESTUDY.Interfaces
{
    public interface IRefundRepository
    {
        Task<IEnumerable<Refund>> GetRefundsByUserIdAsync(int userId);
        Task<Refund> GetRefundByIdAsync(int id);
        Task RequestRefundAsync(Refund refund);
        Task UpdateRefundStatusAsync(int refundId, string status);
    }
}
