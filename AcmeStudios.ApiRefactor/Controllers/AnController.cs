using System.Threading.Tasks;
using AcmeStudios.ApiRefactor.DTOs;
using AcmeStudios.ApiRefactor.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AcmeStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public class AnController : ControllerBase
    {
        private readonly IInterfaceWithDatabase _iwd;
    
        public AnController(IInterfaceWithDatabase iwd)
        {
            _iwd = iwd;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _iwd.GetAllStudioHeaderItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _iwd.GetStudioItemById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {
            return Ok(await _iwd.AddStudioItem(studioItem));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudioItemDto studioItem)
        {
            return Ok(await _iwd.UpdateStudioItem(studioItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _iwd.DeleteStudioItem(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            return Ok(await _iwd.GetAllStudioItemTypes());
        }
    }
}