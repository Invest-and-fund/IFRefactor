using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.Application.DTOs;
using AcmeStudios.ApiRefactor.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcmeStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public sealed class AnController : ControllerBase
    {
        private readonly IStudioItemService _studioItemService;
        private readonly IStudioItemTypeService _studioItemTypeService;

        public AnController(IStudioItemService studioItemService, IStudioItemTypeService studioItemTypeService)
        {
            _studioItemService = studioItemService;
            _studioItemTypeService = studioItemTypeService;
        }

        [HttpGet("GetAll")]
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

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudioItemDto studioItem)
        {
            // TODO I probably want to include the ID in the request here and check that the ID's match
            return Ok(await _studioItemService.UpdateStudioItemAsync(studioItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _studioItemService.DeleteStudioItemAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            return Ok(await _studioItemTypeService.GetAllStudioItemTypesAsync());
        }
    }
}