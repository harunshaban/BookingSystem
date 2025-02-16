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
    public class ResourcesControllerTests
    {
        private readonly Mock<IResourceService> _mockResourceService;
        private readonly ResourcesController _controller;

        public ResourcesControllerTests()
        {
            _mockResourceService = new Mock<IResourceService>();
            _controller = new ResourcesController(_mockResourceService.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenResourcesExist()
        {
            // Arrange
            var resources = new List<ResourceDto>
        {
            new ResourceDto { Id = 1, Name = "Resource 1" },
            new ResourceDto { Id = 2, Name = "Resource 2" }
        };
            _mockResourceService.Setup(service => service.GetAllResources())
                .ReturnsAsync(resources);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedResources = Assert.IsType<List<ResourceDto>>(okResult.Value);
            Assert.Equal(2, returnedResources.Count);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenResourceExists()
        {
            // Arrange
            var resource = new ResourceDto { Id = 1, Name = "Resource 1" };
            _mockResourceService.Setup(service => service.GetResourceById(1))
                .ReturnsAsync(resource);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedResource = Assert.IsType<ResourceDto>(okResult.Value);
            Assert.Equal(1, returnedResource.Id);
            Assert.Equal("Resource 1", returnedResource.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenResourceDoesNotExist()
        {
            // Arrange
            _mockResourceService.Setup(service => service.GetResourceById(999))
                .ReturnsAsync((ResourceDto)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
