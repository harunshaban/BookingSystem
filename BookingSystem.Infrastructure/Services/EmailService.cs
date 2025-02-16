using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure.Services
{
    public class EmailService
    {
        public Task SendBookingConfirmationEmail(int bookingId)
        {
            Console.WriteLine($"EMAIL SENT TO admin@admin.com FOR CREATED BOOKING WITH ID {bookingId}");
            return Task.CompletedTask;
        }
    }
}
