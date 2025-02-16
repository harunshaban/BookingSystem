using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Infrastructure.Services
{
    public class BookingValidationService : IBookingValidationService
    {
        public bool IsAvailable(Resource resource, int requestedQuantity, List<Booking> overlappingBookings)
        {
            int maxBookedQuantity = overlappingBookings
                .GroupBy(b => b.ResourceId)
                .Select(g => g.Sum(b => b.BookedQuantity))
                .FirstOrDefault();

            var result = (resource.Quantity - maxBookedQuantity) >= requestedQuantity;

            return result;
        }
    }
}
