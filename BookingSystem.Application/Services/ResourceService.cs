using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<List<ResourceDto>> GetAllResources()
        {
            var resources = await _resourceRepository.GetAllAsync();
            return resources.Select(r => new ResourceDto
            {
                Id = r.Id,
                Name = r.Name,
                Quantity = r.Quantity
            }).ToList();
        }

        public async Task<ResourceDto> GetResourceById(int id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id);
            if (resource == null)
                return null!;

            return new ResourceDto
            {
                Id = resource.Id,
                Name = resource.Name,
                Quantity = resource.Quantity
            };
        }
    }
}
