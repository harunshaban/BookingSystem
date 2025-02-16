using BookingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Interfaces
{
    public interface IBookingService
    {
        Task<(bool success, string message)> CreateBooking(CreateBookingDto bookingDto);
    }
}
