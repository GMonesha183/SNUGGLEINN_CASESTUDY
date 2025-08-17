using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using SNUGGLEINN_CASESTUDY.DTOs;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace SNUGGLEINN_CASESTUDY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefundController : ControllerBase
    {
        private readonly IRefundRepository _refundRepository;
        private readonly IEmailService _emailService;

        public RefundController(IRefundRepository refundRepository, IEmailService emailService)
        {
            _refundRepository = refundRepository;
            _emailService = emailService;
        }

        // GET: api/refund/my
        [Authorize(Roles = "Guest,Owner,Admin")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyRefunds()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var refunds = await _refundRepository.GetRefundsByUserIdAsync(userId);

            var refundDtos = refunds.Select(r => new RefundDto
            {
                RefundId = r.RefundId,
                BookingId = r.BookingId,
                UserId = r.UserId,
                Amount = r.Amount,
                Status = r.Status,
                RequestedAt = r.RequestedAt,
                ProcessedAt = r.ProcessedAt
            }).ToList();

            return Ok(refundDtos);
        }

        // GET: api/refund/{id}
        [Authorize(Roles = "Guest,Owner,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRefundById(int id)
        {
            var refund = await _refundRepository.GetRefundByIdAsync(id);
            if (refund == null) return NotFound();

            if (User.IsInRole("Guest") && refund.UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Forbid();

            var refundDto = new RefundDto
            {
                RefundId = refund.RefundId,
                BookingId = refund.BookingId,
                UserId = refund.UserId,
                Amount = refund.Amount,
                Status = refund.Status,
                RequestedAt = refund.RequestedAt,
                ProcessedAt = refund.ProcessedAt
            };

            return Ok(refundDto);
        }

        // POST: api/refund/request
        [Authorize(Roles = "Guest,Owner,Admin")]
        [HttpPost("request")]
        public async Task<IActionResult> RequestRefund([FromBody] RefundCreateDto dto)
        {
            if (dto.Amount <= 0 || dto.BookingId <= 0)
                return BadRequest(new { message = "BookingId and Amount must be valid" });

            var refund = new Refund
            {
                BookingId = dto.BookingId,
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Amount = dto.Amount,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow,
                ProcessedAt = DateTime.UtcNow
            };

            await _refundRepository.RequestRefundAsync(refund);

            var resultDto = new RefundDto
            {
                RefundId = refund.RefundId,
                BookingId = refund.BookingId,
                UserId = refund.UserId,
                Amount = refund.Amount,
                Status = refund.Status,
                RequestedAt = refund.RequestedAt,
                ProcessedAt = refund.ProcessedAt
            };

            return Ok(resultDto);
        }

        // PUT: api/refund/update-status/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateRefundStatus(int id, [FromBody] RefundUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Status))
                return BadRequest(new { message = "Status is required" });

            var refund = await _refundRepository.GetRefundByIdAsync(id);
            if (refund == null) return NotFound();

            await _refundRepository.UpdateRefundStatusAsync(id, dto.Status);

            // ✅ Send email if approved
            if (dto.Status.ToLower() == "approved" && !string.IsNullOrEmpty(refund.User?.Email))
            {
                await _emailService.SendEmailAsync(
                    refund.User.Email,
                    "Refund Approved",
                    $"Your refund for Booking ID {refund.BookingId} has been approved. Amount: ₹{refund.Amount}."
                );
            }

            return Ok(new { message = $"Refund status updated to {dto.Status}" });
        }

        // GET: api/refund
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllRefunds()
        {
            var refunds = await _refundRepository.GetAllRefundsAsync();

            var refundDtos = refunds.Select(r => new RefundDto
            {
                RefundId = r.RefundId,
                BookingId = r.BookingId,
                UserId = r.UserId,
                Amount = r.Amount,
                Status = r.Status,
                RequestedAt = r.RequestedAt,
                ProcessedAt = r.ProcessedAt
            }).ToList();

            return Ok(refundDtos);
        }
    }
}
