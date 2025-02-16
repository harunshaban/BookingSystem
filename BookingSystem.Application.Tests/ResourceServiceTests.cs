using BookingSystem.Application.DTOs;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Tests
{
    public class ResourceServiceTests
    {
        private readonly Mock<IResourceRepository> _mockResourceRepository;
        private readonly ResourceService _resourceService;

        public ResourceServiceTests()
        {
            _mockResourceRepository = new Mock<IResourceRepository>();
            _resourceService = new ResourceService(_mockResourceRepository.Object);
        }

        [Fact]
        public async Task GetResourceById_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            var resourceId = 999;
            _mockResourceRepository
                .Setup(r => r.GetByIdAsync(resourceId))
                .ReturnsAsync((Resource)null);

            // Act
            var result = await _resourceService.GetResourceById(resourceId);

            // Assert
            result.Should().BeNull();
        }
    }
}
