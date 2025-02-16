using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Tests.TestSetup
{
    public static class TestDbContext
    {
        public static BookingSystemContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<BookingSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new BookingSystemContext(options);
        }
    }
}
