using AcemStudios.ApiRefactor.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcemStudios.ApiRefactor.Controllers
{
    [Route("peoplespartnership/api/[controller]")]
    [ApiController]
    public class AnController : ControllerBase
    {
        public AnController()
        {

        }

        DatabaseService databaseService = new DatabaseService();

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await databaseService.GetAllStudioHeaderItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await databaseService.GetStudioItemById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudioItemDto studioItem)
        {

            return Ok(await databaseService.AddStudioItem(studioItem));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudioItemDto studioItem)
        {
            return Ok(await databaseService.UpdateStudioItem(studioItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await databaseService.DeleteStudioItem(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetStudioItemTypes()
        {

            return Ok(await databaseService.GetAllStudioItemTypes());
        }
    }
}