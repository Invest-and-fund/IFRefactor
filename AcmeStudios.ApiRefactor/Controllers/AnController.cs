using System.Threading.Tasks;

using AcemStudios.ApiRefactor.DTOs;

using AcmeStudios.ApiRefactor.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace AcemStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public class AnController : ControllerBase
    {
        private readonly IInterfaceWithDatabase _interfaceWithDatabase;

        public AnController(IInterfaceWithDatabase interfaceWithDatabase)
        {
            _interfaceWithDatabase = interfaceWithDatabase;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _interfaceWithDatabase.GetAllStudioHeaderItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _interfaceWithDatabase.GetStudioItemById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {
            return Ok(await _interfaceWithDatabase.AddStudioItem(studioItem));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudioItemDto studioItem)
        {
            return Ok(await _interfaceWithDatabase.UpdateStudioItem(studioItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _interfaceWithDatabase.DeleteStudioItem(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            return Ok(await _interfaceWithDatabase.GetAllStudioItemTypes());
        }
    }
}