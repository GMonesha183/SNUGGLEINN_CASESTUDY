using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Task<IEnumerable<Review>> GetReviewsByHotelIdAsync(int hotelId)
        {
            return _reviewRepository.GetReviewsByHotelIdAsync(hotelId);
        }

        public Task<Review> GetReviewByIdAsync(int id)
        {
            return _reviewRepository.GetReviewByIdAsync(id);
        }

        public Task AddReviewAsync(Review review)
        {
            return _reviewRepository.AddReviewAsync(review);
        }

        public Task UpdateReviewAsync(Review review)
        {
            return _reviewRepository.UpdateReviewAsync(review);
        }

        public Task DeleteReviewAsync(int id)
        {
            return _reviewRepository.DeleteReviewAsync(id);
        }
    }
}
