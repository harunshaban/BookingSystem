using BookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetOverlappingBookings(int resourceId, DateTime dateFrom, DateTime dateTo);
        Task<Booking> CreateBooking(Booking booking);
    }
}
