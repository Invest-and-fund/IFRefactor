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
            return Ok(await _studioItemService.GetAllStudioItemHeadersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _studioItemService.GetStudioItemByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {
            return Ok(await _studioItemService.AddStudioItemAsync(studioItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudioItemDto studioItem)
        {
            if (id != studioItem.StudioItemId)
            {
                return BadRequest("The Id's in the URL and the request body do not match");
            }

            return Ok(await _studioItemService.UpdateStudioItemAsync(studioItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _studioItemService.DeleteStudioItemAsync(id));
        }
    }
}