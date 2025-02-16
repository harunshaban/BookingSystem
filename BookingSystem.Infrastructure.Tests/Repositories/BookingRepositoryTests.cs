using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Repositories;
using BookingSystem.Infrastructure.Tests.TestSetup;

namespace BookingSystem.Infrastructure.Tests.Repositories
{
    public class BookingRepositoryTests : IDisposable
    {
        private readonly BookingSystemContext _context;
        private readonly BookingRepository _repository;

        public BookingRepositoryTests()
        {
            _context = TestDbContext.CreateDbContext();
            _repository = new BookingRepository(_context);
        }

        [Fact]
        public async Task GetOverlappingBookings_ReturnsOverlappingBookings()
        {
            // Arrange
            var resource = new Resource("Test Resource", 10);
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            var bookings = new List<Booking>
            {
                new Booking(DateTime.Today, DateTime.Today.AddDays(2), 2, resource.Id),
                new Booking(DateTime.Today.AddDays(3), DateTime.Today.AddDays(5), 3, resource.Id),
                new Booking(DateTime.Today.AddDays(6), DateTime.Today.AddDays(8), 4, resource.Id)
            };
            _context.Bookings.AddRange(bookings);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetOverlappingBookings(
                resource.Id,
                DateTime.Today.AddDays(1),
                DateTime.Today.AddDays(4)
            );

            // Assert
            Assert.Equal(2, result.Count); // Should return first two bookings
        }

        [Fact]
        public async Task CreateBooking_SavesBookingToDatabase()
        {
            // Arrange
            var resource = new Resource("Test Resource", 10);
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            var booking = new Booking(DateTime.Today, DateTime.Today.AddDays(1), 2, resource.Id);

            // Act
            var result = await _repository.CreateBooking(booking);

            // Assert
            Assert.NotEqual(0, result.Id); // Ensure ID was assigned
            var savedBooking = await _context.Bookings.FindAsync(result.Id);
            Assert.NotNull(savedBooking);
            Assert.Equal(booking.DateFrom.Date, savedBooking.DateFrom.Date);
            Assert.Equal(booking.DateTo.Date, savedBooking.DateTo.Date);
            Assert.Equal(booking.BookedQuantity, savedBooking.BookedQuantity);
        }

        [Fact]
        public async Task CreateBooking_WhenBookingIsNull_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.CreateBooking(null));
        }

        [Fact]
        public async Task CreateBooking_WhenDateFromIsGreaterThanDateTo_ThrowsArgumentException()
        {
            // Arrange
            var resource = new Resource("Test Resource", 10);
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            var booking = new Booking(DateTime.Today.AddDays(1), DateTime.Today, 2, resource.Id);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.CreateBooking(booking));
        }

        [Fact]
        public async Task CreateBooking_WhenBookedQuantityIsZeroOrNegative_ThrowsArgumentException()
        {
            // Arrange
            var resource = new Resource("Test Resource", 10);
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            var booking = new Booking(DateTime.Today, DateTime.Today.AddDays(1), 0, resource.Id);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.CreateBooking(booking));
        }

        [Fact]
        public async Task CreateBooking_WhenResourceDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var booking = new Booking(DateTime.Today, DateTime.Today.AddDays(1), 2, 999); // Non-existent resource ID

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _repository.CreateBooking(booking));
        }

        [Fact]
        public async Task GetOverlappingBookings_WhenResourceDoesNotExist_ReturnsEmptyList()
        {
            // Act
            var result = await _repository.GetOverlappingBookings(
                999, // Non-existent resource ID
                DateTime.Today,
                DateTime.Today.AddDays(1)
            );

            // Assert
            Assert.Empty(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
