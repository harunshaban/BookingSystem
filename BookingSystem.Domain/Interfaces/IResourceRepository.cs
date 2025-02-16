using BookingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Interfaces
{
    public interface IResourceRepository
    {
        Task<Resource> GetByIdAsync(int id);
        Task<List<Resource>> GetAllAsync();
    }
}
