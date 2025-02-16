using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Interfaces;
using BookingSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IBookingValidationService _validationService;
        private readonly EmailService _emailService;

        public BookingService(
            IBookingRepository bookingRepository,
            IResourceRepository resourceRepository,
            IBookingValidationService validationService,
            EmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _resourceRepository = resourceRepository;
            _validationService = validationService;
            _emailService = emailService;
        }

        public async Task<(bool success, string message)> CreateBooking(CreateBookingDto bookingDto)
        {
            var resource = await _resourceRepository.GetByIdAsync(bookingDto.ResourceId);
            if (resource == null)
                return (false, "Resource not found");

            var overlapingBooking = await _bookingRepository.GetOverlappingBookings(
                bookingDto.ResourceId, bookingDto.DateFrom, bookingDto.DateTo);

            if (!_validationService.IsAvailable(resource, bookingDto.BookedQuantity, overlapingBooking))
                return (false, "Requested quantity is not available for the selected period");

            var booking = new Booking(
                bookingDto.DateFrom,
                bookingDto.DateTo,
                bookingDto.BookedQuantity,
                bookingDto.ResourceId);

            var createdBooking = await _bookingRepository.CreateBooking(booking);
            await _emailService.SendBookingConfirmationEmail(createdBooking.Id);

            return (true, "Booking created successfully");
        }
    }
}
