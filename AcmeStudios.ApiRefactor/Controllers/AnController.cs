using System.Collections.Generic;
using System.Threading.Tasks;

using AcemStudios.ApiRefactor.DTOs;

using AcmeStudios.ApiRefactor.Contracts;
using AcmeStudios.ApiRefactor.DTOs;

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
        public async Task<ActionResult<List<StudioItemHeaderDto>>> Get()
        {
            return Ok(await _interfaceWithDatabase.GetAllStudioHeaderItems());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudioItemDto>> GetById(int id)
        {
            return Ok(await _interfaceWithDatabase.GetStudioItemById(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<StudioItemDto>>> Add(StudioItemForCreationDto studioItem)
        {
            return Ok(await _interfaceWithDatabase.AddStudioItem(studioItem));
        }

        [HttpPut]
        public async Task<ActionResult> Update(StudioItemForUpdateDto studioItem)
        {
            return Ok(await _interfaceWithDatabase.UpdateStudioItem(studioItem));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _interfaceWithDatabase.DeleteStudioItem(id));
        }

        [HttpGet]
        public async Task<ActionResult<StudioItemTypeDto>> GetStudioItemTypes()
        {
            return Ok(await _interfaceWithDatabase.GetAllStudioItemTypes());
        }
    }
}