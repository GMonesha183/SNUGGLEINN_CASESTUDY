using Microsoft.EntityFrameworkCore;
using SNUGGLEINN_CASESTUDY.Data;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Repositories
{
    public class RefundRepository : IRefundRepository
    {
        private readonly ApplicationDbContext _context;

        public RefundRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Refund>> GetRefundsByUserIdAsync(int userId)
        {
            return await _context.Refunds
                                 .Include(r => r.Booking)
                                 .ThenInclude(b => b.Room)
                                 .Include(r => r.User) // Include User for email
                                 .Where(r => r.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<Refund> GetRefundByIdAsync(int id)
        {
            return await _context.Refunds
                                 .Include(r => r.Booking)
                                 .ThenInclude(b => b.Room)
                                 .Include(r => r.User) // Include User for email
                                 .FirstOrDefaultAsync(r => r.RefundId == id);
        }

        public async Task RequestRefundAsync(Refund refund)
        {
            _context.Refunds.Add(refund);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRefundStatusAsync(int refundId, string status)
        {
            var refund = await _context.Refunds
                                       .Include(r => r.User) // Include User for email
                                       .FirstOrDefaultAsync(r => r.RefundId == refundId);

            if (refund != null)
            {
                refund.Status = status;
                _context.Refunds.Update(refund);
                await _context.SaveChangesAsync();

                // ✅ Send email if approved
                if (status.ToLower() == "approved" && refund.User != null)
                {
                    // You can call your EmailService here
                    // Example:
                    // await _emailService.SendEmailAsync(refund.User.Email, "Refund Approved", $"Your refund for booking {refund.BookingId} has been approved.");
                }
            }
        }

        public async Task<IEnumerable<Refund>> GetAllRefundsAsync()
        {
            return await _context.Refunds
                                 .Include(r => r.Booking)
                                 .ThenInclude(b => b.Room)
                                 .Include(r => r.User) // Include User for email
                                 .ToListAsync();
        }
    }
}
