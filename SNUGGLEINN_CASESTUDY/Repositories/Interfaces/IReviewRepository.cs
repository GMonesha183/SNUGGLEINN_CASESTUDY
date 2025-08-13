using System.Collections.Generic;
using System.Threading.Tasks;
using SNUGGLEINN_CASESTUDY.Models;

namespace SNUGGLEINN_CASESTUDY.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsByHotelIdAsync(int hotelId);
        Task<Review> GetReviewByIdAsync(int id);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
    }
}
