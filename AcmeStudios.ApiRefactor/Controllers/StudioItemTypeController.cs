using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcmeStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public sealed class StudioItemTypeController : ControllerBase
    {
        private readonly IStudioItemTypeService _studioItemTypeService;

        public StudioItemTypeController(IStudioItemTypeService studioItemTypeService)
        {
            _studioItemTypeService = studioItemTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            return Ok(await _studioItemTypeService.GetAllStudioItemTypesAsync());
        }
    }
}
