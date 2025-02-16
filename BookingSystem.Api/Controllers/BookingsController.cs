using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingDto bookingDto)
        {
            var (success, message) = await _bookingService.CreateBooking(bookingDto);
            if (!success)
                return BadRequest(message);

            return Ok(message);
        }
    }
}
