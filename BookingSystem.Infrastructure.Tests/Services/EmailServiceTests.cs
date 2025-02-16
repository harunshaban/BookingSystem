using BookingSystem.Infrastructure.Services;

namespace BookingSystem.Infrastructure.Tests.Services
{
    public class EmailServiceTests
    {
        private readonly EmailService _emailService;
        private readonly StringWriter _stringWriter;

        public EmailServiceTests()
        {
            _emailService = new EmailService();
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Fact]
        public async Task SendBookingConfirmationEmail_WritesToConsole()
        {
            // Arrange
            int bookingId = 123;

            // Act
            await _emailService.SendBookingConfirmationEmail(bookingId);
            var output = _stringWriter.ToString();

            // Assert
            Assert.Contains($"EMAIL SENT TO admin@admin.com FOR CREATED BOOKING WITH ID {bookingId}", output);
        }

        public void Dispose()
        {
            _stringWriter.Dispose();
        }
    }
}
