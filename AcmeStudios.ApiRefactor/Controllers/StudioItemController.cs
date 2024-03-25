using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AcmeStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    [EnableCors("AllowMyOrigin")]
    public class StudioItemController : ControllerBase
    {
        private readonly StudioItemServices _studioItemServices;
        private readonly ILogger<StudioItemController> _logger;

        public StudioItemController(StudioItemServices studioItemServices, 
            ILogger<StudioItemController> logger)
        {
            _studioItemServices = studioItemServices;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _studioItemServices.GetAllStudioHeaderItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _studioItemServices.GetStudioItemById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {
            return Ok(await _studioItemServices.AddStudioItem(studioItem));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudioItemDto studioItem)
        {
            return Ok(await _studioItemServices.UpdateStudioItem(studioItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _studioItemServices.DeleteStudioItem(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            return Ok(await _studioItemServices.GetAllStudioItemTypes());
        }
    }
}