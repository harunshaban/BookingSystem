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
    public class ResourceRepository : IResourceRepository
    {
        private readonly BookingSystemContext _context;

        public ResourceRepository(BookingSystemContext context)
        {
            _context = context;
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            var result = await _context.Resources.ToListAsync();
            return result;
        }

        public async Task<Resource> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero", nameof(id));

            var result = await _context.Resources.FindAsync(id);

            return result!;
        }
    }
}
