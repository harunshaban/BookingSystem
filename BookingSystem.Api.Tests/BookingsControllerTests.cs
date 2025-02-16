using BookingSystem.Api.Controllers;
using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Api.Tests
{
    public class BookingsControllerTests
    {
        private readonly Mock<IBookingService> _mockBookingService;
        private readonly BookingsController _controller;

        public BookingsControllerTests()
        {
            _mockBookingService = new Mock<IBookingService>();
            _controller = new BookingsController(_mockBookingService.Object);
        }

        [Fact]
        public async Task CreateBooking_ShouldReturnOk_WhenBookingIsSuccessful()
        {
            // Arrange
            var bookingDto = new CreateBookingDto { /* Set properties */ };
            _mockBookingService.Setup(service => service.CreateBooking(bookingDto))
                .ReturnsAsync((true, "Booking created successfully"));

            // Act
            var result = await _controller.Create(bookingDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Booking created successfully", okResult.Value);
        }

        [Fact]
        public async Task CreateBooking_ShouldReturnBadRequest_WhenBookingFails()
        {
            // Arrange
            var bookingDto = new CreateBookingDto { /* Set properties */ };
            _mockBookingService.Setup(service => service.CreateBooking(bookingDto))
                .ReturnsAsync((false, "Booking failed due to unavailable slot"));

            // Act
            var result = await _controller.Create(bookingDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Booking failed due to unavailable slot", badRequestResult.Value);
        }
    }
}
