using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNUGGLEINN_CASESTUDY.DTOs;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;

namespace SNUGGLEINN_CASESTUDY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/review/hotel/{hotelId} -> Fetch all reviews for a hotel (exclude deleted)
        [AllowAnonymous]
        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetReviewsByHotelId(int hotelId)
        {
            try
            {
                Console.WriteLine($"Fetching reviews for hotel {hotelId}...");

                var reviews = await _reviewRepository.GetReviewsByHotelIdAsync(hotelId);

                if (reviews == null)
                {
                    Console.WriteLine("Repository returned null.");
                    reviews = new List<Review>();
                }

                Console.WriteLine($"Repository returned {reviews.Count()} reviews.");

                var reviewList = reviews.Where(r => r != null && !r.IsDeleted).ToList();
                Console.WriteLine($"Filtered to {reviewList.Count} non-deleted reviews.");

                var reviewDtos = reviewList.Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    HotelId = r.HotelId,
                    UserId = r.UserId,
                    Rating = r.Rating,
                    Comment = r.Comment ?? "",
                    CreatedAt = r.CreatedAt,
                    FilePath = r.FilePath ?? ""
                }).ToList();

                return Ok(reviewDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetReviewsByHotelId] Exception: {ex}");
                return StatusCode(500, new { message = "An error occurred while fetching reviews." });
            }
        }


        // GET: api/review/{id} -> Fetch a single review
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            try
            {
                var review = await _reviewRepository.GetReviewByIdAsync(id);
                if (review == null || review.IsDeleted) return NotFound();

                var reviewDto = new ReviewDto
                {
                    ReviewId = review.ReviewId,
                    HotelId = review.HotelId,
                    UserId = review.UserId,
                    Rating = review.Rating,
                    Comment = review.Comment ?? "",
                    CreatedAt = review.CreatedAt,
                    FilePath = review.FilePath ?? ""
                };

                return Ok(reviewDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetReviewById] {ex}");
                return StatusCode(500, new { message = "An error occurred while fetching the review." });
            }
        }

        // POST: api/review/add -> Add review (Guest, Owner, Admin)
        [Authorize(Roles = "Guest,Owner,Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddReview([FromForm] ReviewCreateDto reviewDto, IFormFile file)
        {
            try
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(userIdClaim, out int userId))
                    return Unauthorized(new { message = "Invalid user ID." });

                var review = new Review
                {
                    HotelId = reviewDto.HotelId,
                    UserId = userId,
                    Rating = reviewDto.Rating,
                    Comment = reviewDto.Comment ?? "",
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false,
                    FilePath = null
                };

                // Handle optional file upload
                if (file != null && file.Length > 0)
                {
                    var uploads = Path.Combine("wwwroot", "uploads");
                    if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    review.FilePath = $"uploads/{fileName}";
                }

                await _reviewRepository.AddReviewAsync(review);
                return Ok(new { message = "Review added successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AddReview] {ex}");
                return StatusCode(500, new { message = "An error occurred while adding the review." });
            }
        }

        // PUT: api/review/update/{id} -> Update review
        [Authorize(Roles = "Guest,Owner,Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewUpdateDto reviewDto)
        {
            try
            {
                var existingReview = await _reviewRepository.GetReviewByIdAsync(id);
                if (existingReview == null || existingReview.IsDeleted) return NotFound();

                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(userIdClaim, out int userId))
                    return Unauthorized(new { message = "Invalid user ID." });

                // Guests can only update their own reviews
                if (User.IsInRole("Guest") && existingReview.UserId != userId)
                    return Forbid();

                existingReview.Rating = reviewDto.Rating;
                existingReview.Comment = reviewDto.Comment ?? "";

                await _reviewRepository.UpdateReviewAsync(existingReview);
                return Ok(new { message = "Review updated successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UpdateReview] {ex}");
                return StatusCode(500, new { message = "An error occurred while updating the review." });
            }
        }

        // DELETE: api/review/delete/{id} -> Soft delete
        [Authorize(Roles = "Guest,Owner,Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var existingReview = await _reviewRepository.GetReviewByIdAsync(id);
                if (existingReview == null || existingReview.IsDeleted) return NotFound();

                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(userIdClaim, out int userId))
                    return Unauthorized(new { message = "Invalid user ID." });

                // Only Admin, Owner, or review owner (Guest) can delete
                if (User.IsInRole("Guest") && existingReview.UserId != userId)
                    return Forbid();

                existingReview.IsDeleted = true;
                await _reviewRepository.UpdateReviewAsync(existingReview);

                return Ok(new { message = "Review deleted successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DeleteReview] {ex}");
                return StatusCode(500, new { message = "An error occurred while deleting the review." });
            }
        }
    }
}
