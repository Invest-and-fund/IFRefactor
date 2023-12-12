using AcemStudios.ApiRefactor.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcemStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public class AnController : ControllerBase
    {
        private InterfaceWithDatabase iwd;

        public AnController(InterfaceWithDatabase iwd)
        {
            this.iwd = iwd;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await iwd.GetAllStudioHeaderItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            return Ok(await iwd.GetStudioItemById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {
            return Ok(await iwd.AddStudioItem(studioItem));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudioItemDto studioItem)
        {

            return Ok(await iwd.UpdateStudioItem(studioItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            return Ok(await iwd.DeleteStudioItem(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            return Ok(await iwd.GetAllStudioItemTypes());
        }
    }
}