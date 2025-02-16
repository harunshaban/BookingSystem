using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace BookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly Application.Interfaces.IResourceService _resourceService;
        public ResourcesController(Application.Interfaces.IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResourceDto>>> GetAll()
        {
            var resources = await _resourceService.GetAllResources();
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetById(int id)
        {
            var resource = await _resourceService.GetResourceById(id);
            if (resource == null)
                return NotFound();

            return Ok(resource);
        }
    }
}
