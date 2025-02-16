using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Interfaces;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingSystemContext _context;

        public BookingRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            if (booking.DateFrom > booking.DateTo)
                throw new ArgumentException("DateFrom cannot be greater than DateTo");

            if (booking.BookedQuantity <= 0)
                throw new ArgumentException("BookedQuantity must be greater than zero");

            var resourceExists = await _context.Resources.AnyAsync(r => r.Id == booking.ResourceId);
            if (!resourceExists)
                throw new InvalidOperationException($"Resource with ID {booking.ResourceId} does not exist");

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<List<Booking>> GetOverlappingBookings(int resourceId, DateTime dateFrom, DateTime dateTo)
        {
            var result = await _context.Bookings
                .Where(b => b.ResourceId == resourceId &&
                            b.DateFrom.Date <= dateTo.Date &&
                            b.DateTo.Date >= dateFrom.Date)
                .ToListAsync();
            return result;
        }
    }
}
