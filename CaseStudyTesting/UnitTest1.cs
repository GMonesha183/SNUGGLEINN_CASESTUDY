using Moq;
using NUnit.Framework;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Tests
{
    // ==============================
    // BookingRepository
    // ==============================
    [TestFixture]
    public class BookingRepositoryTests
    {
        private Mock<IBookingRepository> _mockRepo;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IBookingRepository>();
        }

        [Test]
        public async Task GetBookingsByUserIdAsync_ReturnsBookings()
        {
            var bookings = new List<Booking> { new Booking { BookingId = 1, UserId = 10 } };
            _mockRepo.Setup(r => r.GetBookingsByUserIdAsync(10)).ReturnsAsync(bookings);

            var result = await _mockRepo.Object.GetBookingsByUserIdAsync(10);

            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public async Task GetBookingByIdAsync_ReturnsBooking()
        {
            var booking = new Booking { BookingId = 1 };
            _mockRepo.Setup(r => r.GetBookingByIdAsync(1)).ReturnsAsync(booking);

            var result = await _mockRepo.Object.GetBookingByIdAsync(1);

            Assert.NotNull(result);
        }

        [Test]
        public async Task AddBookingAsync_CallsRepository()
        {
            var booking = new Booking { BookingId = 2 };
            await _mockRepo.Object.AddBookingAsync(booking);

            _mockRepo.Verify(r => r.AddBookingAsync(booking), Times.Once);
        }

        [Test]
        public async Task UpdateBookingAsync_CallsRepository()
        {
            var booking = new Booking { BookingId = 3 };
            await _mockRepo.Object.UpdateBookingAsync(booking);

            _mockRepo.Verify(r => r.UpdateBookingAsync(booking), Times.Once);
        }

        [Test]
        public async Task CancelBookingAsync_CallsRepository()
        {
            await _mockRepo.Object.CancelBookingAsync(1);
            _mockRepo.Verify(r => r.CancelBookingAsync(1), Times.Once);
        }

        [Test]
        public async Task GetBookingsByOwnerIdAsync_ReturnsBookings()
        {
            var bookings = new List<Booking> { new Booking { BookingId = 5 } };
            _mockRepo.Setup(r => r.GetBookingsByOwnerIdAsync(1)).ReturnsAsync(bookings);

            var result = await _mockRepo.Object.GetBookingsByOwnerIdAsync(1);

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public async Task GetAllBookingsAsync_ReturnsBookings()
        {
            var bookings = new List<Booking> { new Booking { BookingId = 6 } };
            _mockRepo.Setup(r => r.GetAllBookingsAsync()).ReturnsAsync(bookings);

            var result = await _mockRepo.Object.GetAllBookingsAsync();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public async Task GetBookingsByRoomIdAsync_ReturnsBookings()
        {
            var bookings = new List<Booking> { new Booking { BookingId = 7 } };
            _mockRepo.Setup(r => r.GetBookingsByRoomIdAsync(2)).ReturnsAsync(bookings);

            var result = await _mockRepo.Object.GetBookingsByRoomIdAsync(2);

            Assert.AreEqual(1, result.Count);
        }
    }

    // ==============================
    // EmailService
    // ==============================
    [TestFixture]
    public class EmailServiceTests
    {
        private Mock<IEmailService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IEmailService>();
        }

        [Test]
        public async Task SendEmailAsync_CallsService()
        {
            await _mockService.Object.SendEmailAsync("to@test.com", "subject", "body");

            _mockService.Verify(s => s.SendEmailAsync("to@test.com", "subject", "body"), Times.Once);
        }
    }

    // ==============================
    // HotelRepository
    // ==============================
    [TestFixture]
    public class HotelRepositoryTests
    {
        private Mock<IHotelRepository> _mockRepo;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IHotelRepository>();
        }

        [Test]
        public async Task GetAllHotelsAsync_ReturnsHotels()
        {
            var hotels = new List<Hotel> { new Hotel { HotelId = 1 } };
            _mockRepo.Setup(r => r.GetAllHotelsAsync()).ReturnsAsync(hotels);

            var result = await _mockRepo.Object.GetAllHotelsAsync();

            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public async Task GetHotelByIdAsync_ReturnsHotel()
        {
            var hotel = new Hotel { HotelId = 2 };
            _mockRepo.Setup(r => r.GetHotelByIdAsync(2)).ReturnsAsync(hotel);

            var result = await _mockRepo.Object.GetHotelByIdAsync(2);

            Assert.NotNull(result);
        }

        [Test]
        public async Task AddHotelAsync_CallsRepository()
        {
            var hotel = new Hotel { HotelId = 3 };
            await _mockRepo.Object.AddHotelAsync(hotel);

            _mockRepo.Verify(r => r.AddHotelAsync(hotel), Times.Once);
        }

        [Test]
        public async Task UpdateHotelAsync_CallsRepository()
        {
            var hotel = new Hotel { HotelId = 4 };
            await _mockRepo.Object.UpdateHotelAsync(hotel);

            _mockRepo.Verify(r => r.UpdateHotelAsync(hotel), Times.Once);
        }

        [Test]
        public async Task DeleteHotelAsync_CallsRepository()
        {
            await _mockRepo.Object.DeleteHotelAsync(5);
            _mockRepo.Verify(r => r.DeleteHotelAsync(5), Times.Once);
        }
    }

    // ==============================
    // RefundRepository
    // ==============================
    [TestFixture]
    public class RefundRepositoryTests
    {
        private Mock<IRefundRepository> _mockRepo;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRefundRepository>();
        }

        [Test]
        public async Task GetRefundsByUserIdAsync_ReturnsRefunds()
        {
            var refunds = new List<Refund> { new Refund { RefundId = 1 } };
            _mockRepo.Setup(r => r.GetRefundsByUserIdAsync(1)).ReturnsAsync(refunds);

            var result = await _mockRepo.Object.GetRefundsByUserIdAsync(1);

            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public async Task GetRefundByIdAsync_ReturnsRefund()
        {
            var refund = new Refund { RefundId = 2 };
            _mockRepo.Setup(r => r.GetRefundByIdAsync(2)).ReturnsAsync(refund);

            var result = await _mockRepo.Object.GetRefundByIdAsync(2);

            Assert.NotNull(result);
        }

        [Test]
        public async Task RequestRefundAsync_CallsRepository()
        {
            var refund = new Refund { RefundId = 3 };
            await _mockRepo.Object.RequestRefundAsync(refund);

            _mockRepo.Verify(r => r.RequestRefundAsync(refund), Times.Once);
        }

        [Test]
        public async Task UpdateRefundStatusAsync_CallsRepository()
        {
            await _mockRepo.Object.UpdateRefundStatusAsync(4, "Approved");
            _mockRepo.Verify(r => r.UpdateRefundStatusAsync(4, "Approved"), Times.Once);
        }

        [Test]
        public async Task GetAllRefundsAsync_ReturnsRefunds()
        {
            var refunds = new List<Refund> { new Refund { RefundId = 5 } };
            _mockRepo.Setup(r => r.GetAllRefundsAsync()).ReturnsAsync(refunds);

            var result = await _mockRepo.Object.GetAllRefundsAsync();

            Assert.AreEqual(1, result.ToList().Count);
        }
    }

    // ==============================
    // ReviewRepository
    // ==============================
    [TestFixture]
    public class ReviewRepositoryTests
    {
        private Mock<IReviewRepository> _mockRepo;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IReviewRepository>();
        }

        [Test]
        public async Task GetReviewsByHotelIdAsync_ReturnsReviews()
        {
            var reviews = new List<Review> { new Review { ReviewId = 1 } };
            _mockRepo.Setup(r => r.GetReviewsByHotelIdAsync(1)).ReturnsAsync(reviews);

            var result = await _mockRepo.Object.GetReviewsByHotelIdAsync(1);

            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public async Task GetReviewByIdAsync_ReturnsReview()
        {
            var review = new Review { ReviewId = 2 };
            _mockRepo.Setup(r => r.GetReviewByIdAsync(2)).ReturnsAsync(review);

            var result = await _mockRepo.Object.GetReviewByIdAsync(2);

            Assert.NotNull(result);
        }

        [Test]
        public async Task AddReviewAsync_CallsRepository()
        {
            var review = new Review { ReviewId = 3 };
            await _mockRepo.Object.AddReviewAsync(review);

            _mockRepo.Verify(r => r.AddReviewAsync(review), Times.Once);
        }

        [Test]
        public async Task UpdateReviewAsync_CallsRepository()
        {
            var review = new Review { ReviewId = 4 };
            await _mockRepo.Object.UpdateReviewAsync(review);

            _mockRepo.Verify(r => r.UpdateReviewAsync(review), Times.Once);
        }

        [Test]
        public async Task DeleteReviewAsync_CallsRepository()
        {
            await _mockRepo.Object.DeleteReviewAsync(5);
            _mockRepo.Verify(r => r.DeleteReviewAsync(5), Times.Once);
        }
    }

    // ==============================
    // RoomRepository
    // ==============================
    [TestFixture]
    public class RoomRepositoryTests
    {
        private Mock<IRoomRepository> _mockRepo;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRoomRepository>();
        }

        [Test]
        public async Task GetRoomsByHotelIdAsync_ReturnsRooms()
        {
            var rooms = new List<Room> { new Room { RoomId = 1 } };
            _mockRepo.Setup(r => r.GetRoomsByHotelIdAsync(1)).ReturnsAsync(rooms);

            var result = await _mockRepo.Object.GetRoomsByHotelIdAsync(1);

            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public async Task GetRoomByIdAsync_ReturnsRoom()
        {
            var room = new Room { RoomId = 2 };
            _mockRepo.Setup(r => r.GetRoomByIdAsync(2)).ReturnsAsync(room);

            var result = await _mockRepo.Object.GetRoomByIdAsync(2);

            Assert.NotNull(result);
        }

        [Test]
        public async Task AddRoomAsync_CallsRepository()
        {
            var room = new Room { RoomId = 3 };
            await _mockRepo.Object.AddRoomAsync(room);

            _mockRepo.Verify(r => r.AddRoomAsync(room), Times.Once);
        }

        [Test]
        public async Task UpdateRoomAsync_CallsRepository()
        {
            var room = new Room { RoomId = 4 };
            await _mockRepo.Object.UpdateRoomAsync(room);

            _mockRepo.Verify(r => r.UpdateRoomAsync(room), Times.Once);
        }

        [Test]
        public async Task DeleteRoomAsync_CallsRepository()
        {
            await _mockRepo.Object.DeleteRoomAsync(5);
            _mockRepo.Verify(r => r.DeleteRoomAsync(5), Times.Once);
        }
    }

    // ==============================
    // UserRepository
    // ==============================
    [TestFixture]
    public class UserRepositoryTests
    {
        private Mock<IUserRepository> _mockRepo;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IUserRepository>();
        }

        [Test]
        public async Task GetAllUsersAsync_ReturnsUsers()
        {
            var users = new List<User> { new User { UserId = 1 } };
            _mockRepo.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await _mockRepo.Object.GetAllUsersAsync();

            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public async Task GetUserByIdAsync_ReturnsUser()
        {
            var user = new User { UserId = 2 };
            _mockRepo.Setup(r => r.GetUserByIdAsync(2)).ReturnsAsync(user);

            var result = await _mockRepo.Object.GetUserByIdAsync(2);

            Assert.NotNull(result);
        }

        [Test]
        public async Task GetUserByEmailAsync_ReturnsUser()
        {
            var user = new User { Email = "test@test.com" };
            _mockRepo.Setup(r => r.GetUserByEmailAsync("test@test.com")).ReturnsAsync(user);

            var result = await _mockRepo.Object.GetUserByEmailAsync("test@test.com");

            Assert.NotNull(result);
        }

        [Test]
        public async Task GetUserByEmailAndPasswordAsync_ReturnsUser()
        {
            var user = new User { Email = "test@test.com", Password = "1234" };
            _mockRepo.Setup(r => r.GetUserByEmailAndPasswordAsync("test@test.com", "1234")).ReturnsAsync(user);

            var result = await _mockRepo.Object.GetUserByEmailAndPasswordAsync("test@test.com", "1234");

            Assert.NotNull(result);
        }

        [Test]
        public async Task AddUserAsync_CallsRepository()
        {
            var user = new User { UserId = 3 };
            await _mockRepo.Object.AddUserAsync(user);

            _mockRepo.Verify(r => r.AddUserAsync(user), Times.Once);
        }

        [Test]
        public async Task UpdateUserAsync_CallsRepository()
        {
            var user = new User { UserId = 4 };
            await _mockRepo.Object.UpdateUserAsync(user);

            _mockRepo.Verify(r => r.UpdateUserAsync(user), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_CallsRepository()
        {
            await _mockRepo.Object.DeleteUserAsync(5);
            _mockRepo.Verify(r => r.DeleteUserAsync(5), Times.Once);
        }
    }
}
