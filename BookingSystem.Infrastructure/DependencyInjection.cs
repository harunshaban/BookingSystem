using BookingSystem.Domain.Interfaces;
using BookingSystem.Infrastructure.Repositories;
using BookingSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IBookingValidationService, BookingValidationService>();
            services.AddScoped<EmailService>();
            return services;
        }
    }
}
