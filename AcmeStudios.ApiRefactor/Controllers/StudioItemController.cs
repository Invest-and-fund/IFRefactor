using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.Application.DTOs;
using AcmeStudios.ApiRefactor.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcmeStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public sealed class StudioItemController : ControllerBase
    {
        private readonly IStudioItemService _studioItemService;

        public StudioItemController(IStudioItemService studioItemService)
        {
            _studioItemService = studioItemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _studioItemService.GetAllStudioItemHeadersAsync();
            return result.GetApiResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studioItemService.GetStudioItemByIdAsync(id);
            return result.GetApiResponse();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {
            var result = await _studioItemService.AddStudioItemAsync(studioItem);
            return result.GetApiResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudioItemDto studioItem)
        {
            if (id != studioItem.StudioItemId)
            {
                return BadRequest("The Id's in the URL and the request body do not match");
            }

            var result = await _studioItemService.UpdateStudioItemAsync(studioItem);
            return result.GetApiResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studioItemService.DeleteStudioItemAsync(id);
            return result.GetApiResponse();
        }
    }
}