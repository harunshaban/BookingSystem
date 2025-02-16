using BookingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Interfaces
{
    public interface IResourceService
    {
        Task<List<ResourceDto>> GetAllResources();
        Task<ResourceDto> GetResourceById(int id);
    }
}
