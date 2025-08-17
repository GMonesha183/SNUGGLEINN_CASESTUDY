using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Services
{
    public class RefundService
    {
        private readonly IRefundRepository _refundRepository;

        public RefundService(IRefundRepository refundRepository)
        {
            _refundRepository = refundRepository;
        }

        public Task<IEnumerable<Refund>> GetRefundsByUserIdAsync(int userId)
        {
            return _refundRepository.GetRefundsByUserIdAsync(userId);
        }

        public Task<Refund> GetRefundByIdAsync(int id)
        {
            return _refundRepository.GetRefundByIdAsync(id);
        }

        public Task RequestRefundAsync(Refund refund)
        {
            return _refundRepository.RequestRefundAsync(refund);
        }

        public Task UpdateRefundStatusAsync(int refundId, string status)
        {
            return _refundRepository.UpdateRefundStatusAsync(refundId, status);
        }
    }
}
