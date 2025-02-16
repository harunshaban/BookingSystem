using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Repositories;
using BookingSystem.Infrastructure.Tests.TestSetup;

namespace BookingSystem.Infrastructure.Tests.Repositories
{
    public class ResourceRepositoryTests : IDisposable
    {

        private readonly BookingSystemContext _context;
        private readonly ResourceRepository _repository;

        public ResourceRepositoryTests()
        {
            _context = TestDbContext.CreateDbContext();
            _repository = new ResourceRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsResource_WhenResourceExists()
        {
            // Arrange
            var resource = new Resource("Test Resource", 10);
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(resource.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Resource", result.Name);
            Assert.Equal(10, result.Quantity);
        }

        [Fact]
        public async Task GetByIdAsync_WhenResourceDoesNotExist_ReturnsNull()
        {
            // Act
            var result = await _repository.GetByIdAsync(999); // Non-existent ID

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenIdIsZeroOrNegative_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.GetByIdAsync(0));
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.GetByIdAsync(-1));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllResources()
        {
            // Arrange
            var resources = new List<Resource>
            {
                new Resource("Resource 1", 10),
                new Resource("Resource 2", 20)
            };
            _context.Resources.AddRange(resources);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Name == "Resource 1");
            Assert.Contains(result, r => r.Name == "Resource 2");
        }

        [Fact]
        public async Task GetAllAsync_WhenNoResourcesExist_ReturnsEmptyList()
        {
            // Act
            var result = await _repository.GetAllAsync();

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
