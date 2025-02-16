using BookingSystem.Application.DTOs;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Interfaces;
using BookingSystem.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace BookingSystem.Application.Tests
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly Mock<IResourceRepository> _mockResourceRepository;
        private readonly Mock<IBookingValidationService> _mockValidationService;
        private readonly Mock<EmailService> _mockEmailService;
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockResourceRepository = new Mock<IResourceRepository>();
            _mockValidationService = new Mock<IBookingValidationService>();
            _mockEmailService = new Mock<EmailService>();

            _bookingService = new BookingService(
                _mockBookingRepository.Object,
                _mockResourceRepository.Object,
                _mockValidationService.Object,
                _mockEmailService.Object
            );
        }

        [Fact]
        public async Task CreateBooking_WithNonExistentResource_ShouldFail()
        {
            // Arrange
            var bookingDto = new CreateBookingDto
            {
                ResourceId = 999,
                DateFrom = DateTime.Now.AddDays(1),
                DateTo = DateTime.Now.AddDays(2),
                BookedQuantity = 5
            };

            _mockResourceRepository
                .Setup(r => r.GetByIdAsync(bookingDto.ResourceId))
                .ReturnsAsync((Resource)null);

            // Act
            var result = await _bookingService.CreateBooking(bookingDto);

            // Assert
            result.success.Should().BeFalse();
            result.message.Should().Be("Resource not found");

            // Verify that CreateBooking was never called
            _mockBookingRepository.Verify(r => r.CreateBooking(It.IsAny<Booking>()), Times.Never);
        }

    }
}
