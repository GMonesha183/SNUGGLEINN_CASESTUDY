using Microsoft.EntityFrameworkCore;
using SNUGGLEINN_CASESTUDY.Data;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch all reviews for a hotel (exclude deleted) - safe version
        public async Task<IEnumerable<Review>> GetReviewsByHotelIdAsync(int hotelId)
        {
            try
            {
                return await _context.Reviews
                                     .Where(r => r.HotelId == hotelId && !r.IsDeleted)
                                     .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"[GetReviewsByHotelIdAsync] DB Exception: {dbEx}");
                return new List<Review>();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"[GetReviewsByHotelIdAsync] Exception: {ex}");
                return new List<Review>();
            }
        }

        // Fetch a single review with related user and hotel
        public async Task<Review> GetReviewByIdAsync(int id)
        {
            try
            {
                return await _context.Reviews
                                     .Include(r => r.User)
                                     .Include(r => r.Hotel)
                                     .FirstOrDefaultAsync(r => r.ReviewId == id);
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"[GetReviewByIdAsync] DB Exception: {dbEx}");
                return null;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"[GetReviewByIdAsync] Exception: {ex}");
                return null;
            }
        }

        public async Task AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                review.IsDeleted = true; // Soft delete
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
