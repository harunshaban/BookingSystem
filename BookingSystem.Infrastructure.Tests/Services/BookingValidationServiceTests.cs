using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.Services;

namespace BookingSystem.Infrastructure.Tests.Services
{
    public class BookingValidationServiceTests
    {
        private readonly BookingValidationService _validationService;

        public BookingValidationServiceTests()
        {
            _validationService = new BookingValidationService();
        }

        [Fact]
        public void IsAvailable_WhenNoOverlappingBookings_ReturnsTrue()
        {
            // Arrange
            var resource = new Resource("Test Resource", 10);
            var overlappingBookings = new List<Booking>();
            var requestedQuantity = 5;

            // Act
            var result = _validationService.IsAvailable(resource, requestedQuantity, overlappingBookings);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAvailable_WhenNotEnoughQuantity_ReturnsFalse()
        {
            // Arrange
            var resource = new Resource("Test Resource", 10);
            var overlappingBookings = new List<Booking>
            {
                new Booking(DateTime.Today, DateTime.Today.AddDays(1), 7, 1)
            };
            var requestedQuantity = 4;

            // Act
            var result = _validationService.IsAvailable(resource, requestedQuantity, overlappingBookings);

            // Assert
            Assert.False(result);
        }
    }
}
